using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotterySourceConfigManager : ILotterySourceConfigManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotterySourceConfigManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public List<LotterySourceConfig> GetConfigsForSource(int lotterySourceId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var repo = uow.GetRepository<IRepository<LotterySourceConfigRecord>, LotterySourceConfigRecord>();
                return repo.Where(l => l.LotterySourceId == lotterySourceId).Select(l => new LotterySourceConfig
                {
                    FieldDelimiter = l.FieldDelimiter,
                    FileNamePattern = l.FileNamePattern,
                    Id = l.Id,
                    LotterySourceId = l.LotterySourceId,
                    HeadersCount = l.HeadersCount,
                    FootersCount = l.FootersCount
                }).ToList();
            }
        }
    }
}
