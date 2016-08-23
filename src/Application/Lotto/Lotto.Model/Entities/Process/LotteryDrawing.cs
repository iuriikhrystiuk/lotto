using System.Collections.Generic;
using System.Linq;
using Lotto.Model.Constants;
using Lotto.Model.Constants.Process;
using Lotto.Model.Extensions;
using Lotto.Model.Records;
using Lotto.Model.Records.Process;

namespace Lotto.Model.Entities.Process
{
    public class LotteryDrawing
    {
        private string uniqueIdentifier;

        public LotteryDrawing()
        {
            this.Combination = new List<int>();
        }

        public int Id { get; set; }

        public List<int> Combination { get; set; }

        public Status Status { get; set; }

        public string UniqueIdentifier
        {
            get
            {
                if (string.IsNullOrEmpty(this.uniqueIdentifier))
                {
                    this.uniqueIdentifier = this.Combination.CalculateUniqueString();
                }
                return this.uniqueIdentifier;
            }
        }

        public LotteryDrawingRecord ToRecord()
        {
            return new LotteryDrawingRecord
                   {
                       UniqueIdentifier = this.UniqueIdentifier,
                       Status = this.Status
                   };
        }
    }
}
