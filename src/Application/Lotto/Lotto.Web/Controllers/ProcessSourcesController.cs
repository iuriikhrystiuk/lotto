// <copyright file="ProcessSourcesController.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Web.Models;

namespace Lotto.Web.Controllers
{
    public class ProcessSourcesController : ApiController
    {
        private readonly ILotteryProcessSourceManager processSourceManager;

        public ProcessSourcesController(ILotteryProcessSourceManager processSourceManager)
        {
            this.processSourceManager = processSourceManager;
        }

        [HttpGet]
        public IEnumerable<ProcessSource> Get([FromUri] bool selectNew)
        {
            return this.processSourceManager.Get(selectNew).Select(lp => new ProcessSource
            {
                ConnectionString = lp.ConnectionString,
                Id = lp.Id,
                LotteryId = lp.PrimaryLotteryPrize.LotteryId,
                LotteryName = lp.PrimaryLotteryPrize.Lottery.Name,
                PrimaryLotteryPrizeId = lp.PrimaryLotteryPrizeId,
                Size = lp.PrimaryLotteryPrize.Size
            });
        }

        [HttpPost]
        public void Save([FromBody] IList<ProcessSource> processSources)
        {
            var itemsToSave = processSources.Where(p => !string.IsNullOrEmpty(p.ConnectionString)).Select(p => new LotteryProcessSource
            {
                Id = p.Id,
                ConnectionString = p.ConnectionString,
                PrimaryLotteryPrizeId = p.PrimaryLotteryPrizeId
            }).ToList();
            this.processSourceManager.Save(itemsToSave);
        }
    }
}