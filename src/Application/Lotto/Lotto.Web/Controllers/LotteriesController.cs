using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;

namespace Lotto.Web.Controllers
{
    public class LotteriesController : ApiController
    {
        private readonly ILotteryManager lotteryManager;

        public LotteriesController(ILotteryManager lotteryManager)
        {
            this.lotteryManager = lotteryManager;
        }

        [HttpGet]
        public IEnumerable<Lottery> Get()
        {
            return this.lotteryManager.Get();
        }

        [HttpPost]
        public void Save([FromBody] IEnumerable<Lottery> lotteries)
        {
            this.lotteryManager.Save(lotteries.ToList());
        }
    }
}