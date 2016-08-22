// <copyright file="LotterySourceManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotterySourceManager : ILotterySourceManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotterySourceManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public LotterySource GetSourceForLottery(int lotteryId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotterySourceRecord>, LotterySourceRecord>();
                var source = repo.FirstOrDefault(l => l.LotteryId == lotteryId && l.IsPrimary);
                if (source == null)
                {
                    return null;
                }

                return new LotterySource
                {
                    DownloadUrl = source.DownloadUrl,
                    Id = source.Id,
                    IsPrimary = source.IsPrimary,
                    LotteryId = source.LotteryId
                };
            }
        }
    }
}
