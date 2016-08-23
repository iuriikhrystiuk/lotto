// <copyright file="LottoFacade.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using IK.DataAccess.Interfaces;
using IK.Logging.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Constants;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Entities.Process;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Implementation.Process;
using Lotto.Model.Records.Hub;
using Lotto.Model.Records.Process;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    /// <summary>
    ///     The Keno Facade class.
    /// </summary>
    public class LottoFacade : ILottoFacade
    {
        private readonly IDownloader downloader;
        private readonly IDataGenerator generator;
        private readonly ILogger logger;
        private readonly IDataProvider provider;
        private readonly ILotteryProcessStatusManager statusManager;
        private readonly ILotteryProcessStepsManager stepsManager;
        private readonly IUnitOfWorkFactory unitOfWorkFacory;

        internal LottoFacade(
            ILogger logger,
            IDataGenerator generator,
            IDownloader downloader,
            IDataProvider provider,
            IUnitOfWorkFactory unitOfWorkFacory,
            ILotteryProcessStatusManager statusManager,
            ILotteryProcessStepsManager stepsManager)
        {
            this.logger = logger;
            this.generator = generator;
            this.downloader = downloader;
            this.provider = provider;
            this.unitOfWorkFacory = unitOfWorkFacory;
            this.statusManager = statusManager;
            this.stepsManager = stepsManager;
        }

        public Task StartProcessing(LotteryProcessStatus status, IPauseToken pauseToken, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                this.statusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Started);

                
                List<LotteryDrawing> drawings = this.provider.Provide(status.PrimaryLotteryPrize.LotteryId);
                int counter = 0;

                this.logger.Info("Deleting previous steps for status: {0}.", status.Id);
                this.stepsManager.ClearForStatus(status.Id);

                this.logger.Info("Inserting new steps for status: {0}.", status.Id);
                var steps = drawings.Select(d =>
                {
                    counter++;
                    return new LotteryProcessStep
                    {
                        Description = "Processing lottery drawing #" + counter,
                        LotteryProcessStatusId = status.Id,
                        Status = Status.NotRun
                    };
                }).ToList();
                steps = this.stepsManager.AddRange(steps).ToList();

                this.logger.Info("Extracted {0} lottery drawings.", drawings.Count);
                counter = -1;
                foreach (LotteryDrawing lotteryDrawing in drawings)
                {
                    counter++;
                    try
                    {
                        var step = steps[counter];
                        step.StartDate = DateTime.Now;
                        step.Status = Status.Started;
                        this.stepsManager.Update(step);
                        var duration = await this.processDrawing(lotteryDrawing, status.PrimaryLotteryPrize.Size, counter, status.PrimaryLotteryPrize.ProcessSource.ConnectionString, pauseToken,
                                        cancellationToken);
                        step.EndDate = DateTime.Now;
                        step.Duration = TimeSpan.FromSeconds(duration);
                        step.Status = duration == 0 ? Status.Cancelled : Status.Finished;
                        this.stepsManager.Update(step);
                    }
                    catch (OperationCanceledException)
                    {
                        this.statusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Cancelled);
                        return;
                    }
                }
                this.statusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Finished);
            }, cancellationToken);
        }

        private async Task<double> processDrawing(LotteryDrawing lotteryDrawing, int size, int counter, string connectionString, IPauseToken pauseToken, CancellationToken cancellationToken)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            this.logger.Info("Processing lottery drawing #{0}", counter);
            using (IUnitOfWork uow = this.unitOfWorkFacory.CreateUnitOfWork(new ProcessContextDescriptor(), connectionString))
            {
                try
                {
                    IRepository<LotteryDrawingRecord> repo =
                        uow.GetRepository<IRepository<LotteryDrawingRecord>, LotteryDrawingRecord>();
                    IBulkRepository<StagingCombinationRecord> combRepo =
                        uow.GetRepository<IBulkRepository<StagingCombinationRecord>, StagingCombinationRecord>();
                    LotteryDrawing drawing = lotteryDrawing;
                    using (
                        TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew,
                            new TransactionOptions(), TransactionScopeAsyncFlowOption.Enabled))
                    {
                        LotteryDrawingRecord existingDrawing =
                            repo.FirstOrDefault(ld => ld.UniqueIdentifier == drawing.UniqueIdentifier);
                        if (existingDrawing == null)
                        {
                            this.logger.Info("No existing drawing found. Creating lottery drawing #{0}", counter);
                            existingDrawing = lotteryDrawing.ToRecord();
                            existingDrawing.Status = Status.Started;
                            repo.Add(existingDrawing);
                            await uow.SaveChangesAsync();
                        }

                        this.logger.Info("Found lottery drowing #{0} and status {1}", counter,
                            existingDrawing.Status);
                        if (existingDrawing.Status == Status.Started)
                        {
                            List<Combination> combinations = this.generator.Generate(lotteryDrawing.Combination,
                                size);
                            this.logger.Info("Generated {0} combinations for drawing #{1}", combinations.Count,
                                counter);

                            combRepo.BulkInsert(combinations.Select(c => new StagingCombinationRecord
                            {
                                Size = size,
                                UniqueIdentifier = c.UniqueIdentifier
                            }));
                            this.logger.Info("Inserted combinations into staging table for drawing #{0}", counter);

                            await combRepo.ExecuteStoredProcedureAsync("Lotto.ImportCombinations");
                            this.logger.Info(
                                "Merged combinations from staging table to real table for drawing #{0}", counter);

                            st.Stop();
                            this.logger.Info("Finished Processing drawing #{0}", counter);
                            this.logger.Info("It Took {0}", st.Elapsed);

                            LotteryDrawingRecord drawingToCheck = existingDrawing;
                            existingDrawing = repo.First(d => d.UniqueIdentifier == drawingToCheck.UniqueIdentifier);

                            existingDrawing.Status = Status.Finished;
                            await uow.SaveChangesAsync();
                        }
                        else
                        {
                            await pauseToken.WaitWhilePausedAsync();
                            return 0;
                        }

                        transaction.Complete();
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    // munch it and exit
                    this.logger.Info("The processing of combinations of size {0} was cancelled.", size);
                    throw;
                }
                catch (Exception ex)
                {
                    this.logger.Error(ex, "Error occurred while processing drawing #{0}. Please rerun the tool to process it.", counter);
                }
            }

            await pauseToken.WaitWhilePausedAsync();
            return st.Elapsed.TotalSeconds;
        }
    }
}
