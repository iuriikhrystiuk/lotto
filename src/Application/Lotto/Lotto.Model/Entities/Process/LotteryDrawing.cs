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
                       Status = this.Status,
                       Number1 = this.Combination.ElementAt(0),
                       Number2 = this.Combination.ElementAt(1),
                       Number3 = this.Combination.ElementAt(2),
                       Number4 = this.Combination.ElementAt(3),
                       Number5 = this.Combination.ElementAt(4),
                       Number6 = this.Combination.ElementAt(5),
                       Number7 = this.Combination.ElementAt(6),
                       Number8 = this.Combination.ElementAt(7),
                       Number9 = this.Combination.ElementAt(8),
                       Number10 = this.Combination.ElementAt(9),
                       Number11 = this.Combination.ElementAt(10),
                       Number12 = this.Combination.ElementAt(11),
                       Number13 = this.Combination.ElementAt(12),
                       Number14 = this.Combination.ElementAt(13),
                       Number15 = this.Combination.ElementAt(14),
                       Number16 = this.Combination.ElementAt(15),
                       Number17 = this.Combination.ElementAt(16),
                       Number18 = this.Combination.ElementAt(17),
                       Number19 = this.Combination.ElementAt(18),
                       Number20 = this.Combination.ElementAt(19),
                   };
        }
    }
}
