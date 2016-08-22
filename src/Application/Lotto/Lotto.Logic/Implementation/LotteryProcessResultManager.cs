using System.Collections.Generic;
using System.Linq;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Algorithms.Interfaces;
using Lotto.Logic.Algorithms.Interfaces.Rating;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Entities.Process;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Implementation.Process;
using Lotto.Model.Records.Hub;
using Lotto.Model.Records.Process;

namespace Lotto.Logic.Implementation
{
    public class LotteryProcessResultManager : ILotteryProcessResultManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        private readonly IRatingCalculator ratingCalculator;

        public LotteryProcessResultManager(IUnitOfWorkFactory uowFactory, IRatingCalculator ratingCalculator)
        {
            this.uowFactory = uowFactory;
            this.ratingCalculator = ratingCalculator;
        }

        public IEnumerable<LotteryProcessResult> Get(int lotteryPrizeId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotteryProcessResultRecord>, LotteryProcessResultRecord>();
                return repo.Where(l => l.PrimaryLotteryPrizeId == lotteryPrizeId).Select(l => new LotteryProcessResult
                {
                    Id = l.Id,
                    PrimaryLotteryPrizeId = l.PrimaryLotteryPrizeId,
                    RepeatsCount = l.RepeatsCount,
                    UniqueIdentifier = l.UniqueIdentifier
                });
            }
        }

        public void LoadResults(int lotteryPrizeId, int repeatsCount)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotteryProcessSourceRecord>, LotteryProcessSourceRecord>();
                var processSource = repo.First(p => p.PrimaryLotteryPrize.Id == lotteryPrizeId);
                using (var lottoUow = this.uowFactory.CreateUnitOfWork(new ProcessContextDescriptor(), processSource.ConnectionString))
                {
                    var resultsRepo = lottoUow.GetRepository<IRepository<CombinationRecord>, CombinationRecord>();
                    var maxRepeats = repeatsCount > 1 ? repeatsCount : (resultsRepo.FirstOrDefault() != null ? resultsRepo.Max(c => c.RepeatsCount) : 0);
                    if (maxRepeats > 1)
                    {
                        var processResultsRepo = uow.GetRepository<IRepository<LotteryProcessResultRecord>, LotteryProcessResultRecord>();
                        var combinations = resultsRepo.Where(c => c.RepeatsCount == maxRepeats);
                        var processResults = processResultsRepo.Where(p => p.PrimaryLotteryPrizeId == lotteryPrizeId);
                        foreach (var lotteryProcessResultRecord in processResults)
                        {
                            processResultsRepo.Delete(lotteryProcessResultRecord);
                        }
                        foreach (var combinationRecord in combinations)
                        {
                            processResultsRepo.Add(new LotteryProcessResultRecord
                            {
                                RepeatsCount = (int)combinationRecord.RepeatsCount,
                                UniqueIdentifier = combinationRecord.UniqueIdentifier,
                                PrimaryLotteryPrizeId = lotteryPrizeId
                            });
                        }
                    }
                }
                uow.SaveChanges();
            }
        }

        public IEnumerable<CombinationRating> CalculateSimpleWeights(int lotteryPrizeId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var prizeRepo = uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                var resultsRepo = uow.GetRepository<IRepository<LotteryProcessResultRecord>, LotteryProcessResultRecord>();
                var primaryPrize = prizeRepo.First(p => p.Id == lotteryPrizeId);
                var results = resultsRepo.Where(r => r.PrimaryLotteryPrizeId == lotteryPrizeId).Select(r => new Combination
                {
                    RepeatsCount = r.RepeatsCount,
                    Size = primaryPrize.Size,
                    UniqueIdentifier = r.UniqueIdentifier
                }).ToList();
                var lasPrize = this.getLastPrize(prizeRepo, primaryPrize);
                return this.ratingCalculator.CalculateRating(results, lasPrize.Size, primaryPrize.Size).OrderByDescending(r => r.Rating);
            }
        }

        private LotteryPrizeMapRecord getLastPrize(IRepository<LotteryPrizeMapRecord> repo, LotteryPrizeMapRecord previousPrize)
        {
            var nextPrize = repo.FirstOrDefault(p => p.NextLotteryPrizeId == previousPrize.Id);
            if (nextPrize == null)
            {
                return previousPrize;
            }
            return this.getLastPrize(repo, nextPrize);
        }
    }
}
