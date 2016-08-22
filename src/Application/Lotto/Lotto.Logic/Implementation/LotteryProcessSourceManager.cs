// <copyright file="LotteryProcessSourceManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotteryProcessSourceManager : ILotteryProcessSourceManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotteryProcessSourceManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public IEnumerable<LotteryProcessSource> Get(bool selectNew)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var result = new List<LotteryProcessSource>();
                var prizeMapRepo = uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                var lotteryRepo = uow.GetRepository<IRepository<LotteryRecord>, LotteryRecord>();
                var lotteryProcessSourceRepo = uow.GetRepository<IRepository<LotteryProcessSourceRecord>, LotteryProcessSourceRecord>();
                var primaryPrizes = prizeMapRepo.Where(lp => lp.NextLotteryPrizeId == null);
                foreach (var lotteryPrizeMapRecord in primaryPrizes)
                {
                    var lottery = lotteryRepo.First(l => l.Id == lotteryPrizeMapRecord.LotteryId);
                    var processSource =
                        lotteryProcessSourceRepo.FirstOrDefault(
                            l => l.PrimaryLotteryPrize.Id == lotteryPrizeMapRecord.Id);
                    if (processSource != null || selectNew)
                    {
                        result.Add(new LotteryProcessSource
                        {
                            Id = processSource == null ? 0 : processSource.Id,
                            ConnectionString = processSource == null ? null : processSource.ConnectionString,
                            PrimaryLotteryPrizeId = lotteryPrizeMapRecord.Id,
                            PrimaryLotteryPrize = new LotteryPrizeMap
                            {
                                Id = lotteryPrizeMapRecord.Id,
                                LotteryId = lotteryPrizeMapRecord.LotteryId,
                                Lottery = new Lottery
                                {
                                    Id = lottery.Id,
                                    Name = lottery.Name
                                },
                                Size = lotteryPrizeMapRecord.Size,
                                Prize = lotteryPrizeMapRecord.Prize
                            }
                        });
                    }
                }

                return result;
            }
        }

        public void Save(IList<LotteryProcessSource> items)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var prizeMapRepo = uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                var lotteryProcessSourceRepo = uow.GetRepository<IRepository<LotteryProcessSourceRecord>, LotteryProcessSourceRecord>();
                foreach (var lotteryProcessSource in items)
                {
                    var prizeMap = prizeMapRepo.First(p => p.Id == lotteryProcessSource.PrimaryLotteryPrizeId);
                    if (lotteryProcessSource.Id <= 0)
                    {
                        lotteryProcessSourceRepo.Add(new LotteryProcessSourceRecord
                        {
                            ConnectionString = lotteryProcessSource.ConnectionString,
                            PrimaryLotteryPrize = prizeMap
                        });
                    }
                    else
                    {
                        var lotterySource = lotteryProcessSourceRepo.First(l => l.Id == lotteryProcessSource.Id);
                        lotterySource.ConnectionString = lotteryProcessSource.ConnectionString;
                        lotteryProcessSourceRepo.Update(lotterySource);
                    }
                }

                uow.SaveChanges();
            }
        }
    }
}
