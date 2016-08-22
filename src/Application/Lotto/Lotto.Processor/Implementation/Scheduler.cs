// <copyright file="Scheduler.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IK.Logging.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Constants;
using Lotto.Model.Entities.Hub;
using Lotto.Processor.Entities;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    public class Scheduler
    {
        private readonly ILotteryProcessStatusManager processStatusManager;
        private readonly ILottoFacade lottoFacade;
        private readonly List<ProcessDescriptor> processes;
        private readonly ILogger logger;

        public Scheduler(ILotteryProcessStatusManager processStatusManager, ILottoFacade lottoFacade, ILogger logger)
        {
            this.processStatusManager = processStatusManager;
            this.lottoFacade = lottoFacade;
            this.logger = logger;
            this.processes = new List<ProcessDescriptor>();
        }

        public Task Process(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => this.processInternal(cancellationToken), cancellationToken);
        }

        private void processInternal(CancellationToken cancellationToken)
        {
            while (true)
            {
                IEnumerable<LotteryProcessStatus> statuses = this.processStatusManager.Get().ToList();
                this.logger.Info("Queried data base for changes in status of processing. Total Queued: {0}, Total Cancelling: {1}, Total Stopping: {2}, Total Continuing: {3}",
                    statuses.Count(s => s.Status == Status.Queued),
                    statuses.Count(s => s.Status == Status.Cancelling),
                    statuses.Count(s => s.Status == Status.Stopping),
                    statuses.Count(s => s.Status == Status.Continuing));
                foreach (LotteryProcessStatus status in statuses.Where(s => s.Status == Status.Queued))
                {
                    var pauseToken = new PauseTokenSource();
                    var cancellationTokenSource = new CancellationTokenSource();
                    this.logger.Info("Starting process for status {0}", status.Id);
                    var task = this.lottoFacade.StartProcessing(status, pauseToken.Token, cancellationTokenSource.Token);
                    this.processes.Add(new ProcessDescriptor
                    {
                        CancellationToken = cancellationTokenSource,
                        LotteryProcessStatus = status,
                        PauseToken = pauseToken,
                        Task = task
                    });
                }

                foreach (LotteryProcessStatus status in statuses.Where(s => s.Status == Status.Cancelling))
                {
                    this.logger.Info("Cancelling process for status {0}", status.Id);
                    var currentProcess = this.processes.FirstOrDefault(p => p.LotteryProcessStatus.Id == status.Id);
                    if (currentProcess != null)
                    {
                        currentProcess.Task.ContinueWith(t =>
                        {
                            this.processStatusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Cancelled);
                        });
                        currentProcess.CancellationToken.Cancel();
                        this.processes.Remove(currentProcess);
                    }
                    else
                    {
                        this.processStatusManager.SetStatus(status.Id, Status.Cancelled);
                    }
                }

                foreach (LotteryProcessStatus status in statuses.Where(s => s.Status == Status.Stopping))
                {
                    this.logger.Info("Pausing process for status {0}", status.Id);
                    var currentProcess = this.processes.FirstOrDefault(p => p.LotteryProcessStatus.Id == status.Id);
                    if (currentProcess != null)
                    {
                        currentProcess.Task.ContinueWith(t =>
                        {
                            this.processStatusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Stopped);
                        });
                        currentProcess.PauseToken.IsPaused = true;
                    }
                    else
                    {
                        this.processStatusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Cancelled);
                    }
                }

                foreach (LotteryProcessStatus status in statuses.Where(s => s.Status == Status.Continuing))
                {
                    this.logger.Info("Continuing process for status {0}", status.Id);
                    var currentProcess = this.processes.FirstOrDefault(p => p.LotteryProcessStatus.Id == status.Id);
                    if (currentProcess != null)
                    {
                        currentProcess.Task.ContinueWith(t =>
                        {
                            this.processStatusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Started);
                        });
                        currentProcess.PauseToken.IsPaused = false;
                    }
                    else
                    {
                        this.processStatusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Queued);
                    }
                }

                foreach (LotteryProcessStatus status in statuses.Where(s => s.Status == Status.Started))
                {
                    var currentProcess = this.processes.FirstOrDefault(p => p.LotteryProcessStatus.Id == status.Id);
                    if (currentProcess == null)
                    {
                        this.logger.Info("Status of {0} is Started, but it is not being processed. Queuing process for status {0}", status.Id);
                        this.processStatusManager.SetStatus(status.PrimaryLotteryPrizeId, Status.Queued);
                    }
                }
                Thread.Sleep(30000);
                if (cancellationToken.IsCancellationRequested)
                {
                    foreach (ProcessDescriptor process in this.processes)
                    {
                        process.CancellationToken.Cancel();
                    }
                    Task.WaitAll(this.processes.Select(p => p.Task).ToArray());
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
