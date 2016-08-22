// <copyright file="LotteryPrizeMapManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotteryPrizeMapManager : ILotteryPrizeMapManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotteryPrizeMapManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public IList<LotteryPrizeMap> GetPrizeMapFor(int lotteryId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                return repo.Where(pm => pm.LotteryId == lotteryId).Select(pm => new LotteryPrizeMap
                {
                    Id = pm.Id,
                    LotteryId = pm.LotteryId,
                    NextLotteryPrizeId = pm.NextLotteryPrizeId,
                    NextLotteryPrize = pm.NextLotteryPrize != null ? new LotteryPrizeMap
                    {
                        Prize = pm.NextLotteryPrize.Prize,
                        Size = pm.NextLotteryPrize.Size,
                        LotteryId = pm.NextLotteryPrize.LotteryId
                    } : null,
                    Prize = pm.Prize,
                    Size = pm.Size
                }).ToList();
            }
        }

        public void SaveLotteryPrizeMap(IList<LotteryPrizeMap> prizeMap)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();

                this.addOrUpdatePrizeMap(repo, prizeMap);

                var prizeMaps = prizeMap.Select(x => x.Id);
                var lotteryId = prizeMap.First().LotteryId;
                var itemsToDelete = repo.Where(x => !prizeMaps.Contains(x.Id) && x.LotteryId == lotteryId);
                foreach (var prizeMapRecord in itemsToDelete)
                {
                    repo.Delete(prizeMapRecord);
                }

                uow.SaveChanges();
            }
        }

        private void addOrUpdatePrizeMap(IRepository<LotteryPrizeMapRecord> repo, IList<LotteryPrizeMap> prizeMap)
        {
            var newItems = (from lotteryPrizeMap in prizeMap
                            where lotteryPrizeMap.Id <= 0
                            select new LotteryPrizeMapRecord
                            {
                                Size = lotteryPrizeMap.Size,
                                Prize = lotteryPrizeMap.Prize,
                                LotteryId = lotteryPrizeMap.LotteryId
                            }).ToList();

            foreach (var lotteryPrizeMap in prizeMap)
            {
                LotteryPrizeMapRecord nextLotteryPrize = null;
                if (lotteryPrizeMap.NextLotteryPrize != null)
                {
                    nextLotteryPrize = repo.FirstOrDefault(l => l.Id == lotteryPrizeMap.NextLotteryPrize.Id) ??
                                           newItems.FirstOrDefault(l => l.Prize == lotteryPrizeMap.NextLotteryPrize.Prize && l.Size == lotteryPrizeMap.NextLotteryPrize.Size);  
                }
                 
                if (lotteryPrizeMap.Id > 0)
                {
                    var record = repo.First(l => l.Id == lotteryPrizeMap.Id);
                    record.Size = lotteryPrizeMap.Size;
                    record.Prize = lotteryPrizeMap.Prize;
                    record.LotteryId = lotteryPrizeMap.LotteryId;
                    record.Id = lotteryPrizeMap.Id;
                    record.NextLotteryPrize = nextLotteryPrize;
                    record.NextLotteryPrizeId = nextLotteryPrize == null ? (int?) null : nextLotteryPrize.Id;
                    repo.Update(record);
                }
                else
                {
                    var newItem = newItems.First(l => l.Prize == lotteryPrizeMap.Prize && l.Size == lotteryPrizeMap.Size);
                    newItem.NextLotteryPrize = nextLotteryPrize;
                    repo.Add(newItem);
                }
            }
        }
    }
}
