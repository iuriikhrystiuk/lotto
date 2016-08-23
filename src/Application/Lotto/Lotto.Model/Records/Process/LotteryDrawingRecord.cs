using Lotto.Model.Constants;
using Lotto.Model.Constants.Process;

namespace Lotto.Model.Records.Process
{
    public class LotteryDrawingRecord
    {
        public string UniqueIdentifier { get; set; }

        public Status Status { get; set; }
    }
}