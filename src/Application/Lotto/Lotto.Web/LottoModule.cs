// <copyright file="LottoModule.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using Nancy;

namespace Lotto.Web
{
    public class LottoModule : NancyModule
    {
        public LottoModule()
        {
            this.Get["/"] = _ => this.View["Index.cshtml"];
            this.Get["/Lotteries"] = _ => this.View["Lotteries.cshtml"];
            this.Get["/PrizeMap"] = _ => this.View["PrizeMap.cshtml"];
            this.Get["/ProcessSources"] = _ => this.View["ProcessSources.cshtml"];
            this.Get["/ProcessResults"] = _ => this.View["ProcessResults.cshtml"];
            this.Get["/ProcessStatuses"] = _ => this.View["ProcessStatuses.cshtml"];
        }
    }
}