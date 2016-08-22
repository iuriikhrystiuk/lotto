// <copyright file="ProcessResultsController.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Web.Http;
using Lotto.Logic.Algorithms.Interfaces;
using Lotto.Logic.Algorithms.Interfaces.Rating;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;

namespace Lotto.Web.Controllers
{
    public class ProcessResultsController : ApiController
    {
        private readonly ILotteryProcessResultManager processResultManager;

        public ProcessResultsController(ILotteryProcessResultManager processResultManager)
        {
            this.processResultManager = processResultManager;
        }

        [HttpGet]
        public IEnumerable<LotteryProcessResult> Get(int id)
        {
            return this.processResultManager.Get(id);
        }

        [HttpGet]
        public void LoadResults(int id, [FromUri] int repeatsCount)
        {
            this.processResultManager.LoadResults(id, repeatsCount);
        }

        [HttpGet]
        public IEnumerable<CombinationRating> CalculateSimpleWeights(int id)
        {
            return this.processResultManager.CalculateSimpleWeights(id);
        }
    }
}