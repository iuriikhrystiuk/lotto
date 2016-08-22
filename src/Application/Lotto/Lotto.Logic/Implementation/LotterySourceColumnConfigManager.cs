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
    public class LotterySourceColumnConfigManager : ILotterySourceColumnConfigManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotterySourceColumnConfigManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public List<LotterySourceColumnConfig> GetColumnsForConfig(int sourceConfigId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var configRepo = uow.GetRepository<IRepository<LotterySourceConfigRecord>, LotterySourceConfigRecord>();
                var source = configRepo.First(c => c.Id == sourceConfigId);
                return source.LotterySourceColumns.Select(c => new LotterySourceColumnConfig
                {
                    ColumnName = c.LotterySourceColumnConfig.ColumnName,
                    Description = c.LotterySourceColumnConfig.Description,
                    DotNetTypeName = c.LotterySourceColumnConfig.LotterySourceColumnType.DotNetName,
                    BelongsToCombination = c.LotterySourceColumnConfig.BelongsToCombination,
                    Id = c.LotterySourceColumnConfigId,
                    Order = c.Order,
                    LotterySourceColumnTypeId = c.LotterySourceColumnConfig.LotterySourceColumnTypeId
                }).ToList();
            }
        }
    }
}
