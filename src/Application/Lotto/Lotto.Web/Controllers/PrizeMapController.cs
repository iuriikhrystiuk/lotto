// <copyright file="PrizeMapController.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Web.Http;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;

namespace Lotto.Web.Controllers
{
    public class PrizeMapController : ApiController
    {
        private readonly ILotteryPrizeMapManager lotteryPrizemapManager;

        public PrizeMapController(ILotteryPrizeMapManager lotteryPrizemapManager)
        {
            this.lotteryPrizemapManager = lotteryPrizemapManager;
        }

        [HttpGet]
        public IEnumerable<LotteryPrizeMap> Get(int id)
        {
            return this.lotteryPrizemapManager.GetPrizeMapFor(id);
        }

        [HttpPost]
        public void Save([FromBody] List<LotteryPrizeMap> prizeMap)
        {
            this.lotteryPrizemapManager.SaveLotteryPrizeMap(prizeMap);
        }
    }
}