using System.Collections.Generic;
using System.Linq;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotteryManager : ILotteryManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotteryManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public IEnumerable<Lottery> Get()
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotteryRecord>, LotteryRecord>();
                return repo.All().Select(x => new Lottery
                {
                    Id = x.Id,
                    Name = x.Name
                });
            }
        }

        public void Save(IList<Lottery> lotteries)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotteryRecord>, LotteryRecord>();
                foreach (var lottery in lotteries)
                {
                    var currentLottery = lottery;
                    var lotteryRecord = repo.FirstOrDefault(x => x.Id == currentLottery.Id);
                    if (lotteryRecord == null)
                    {
                        repo.Add(new LotteryRecord { Name = currentLottery.Name });
                    }
                    else
                    {
                        lotteryRecord.Name = currentLottery.Name;
                        repo.Update(lotteryRecord);
                    }
                }

                var lotteryIds = lotteries.Select(x => x.Id);
                var itemsToDelete = repo.Where(x => !lotteryIds.Contains(x.Id));
                foreach (var lotteryRecord in itemsToDelete)
                {
                    repo.Delete(lotteryRecord);
                }

                uow.SaveChanges();
            }
        }
    }
}