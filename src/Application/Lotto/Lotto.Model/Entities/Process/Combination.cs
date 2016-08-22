// <copyright file="Combination.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Model.Extensions;
using Lotto.Model.Records;
using Lotto.Model.Records.Process;

namespace Lotto.Model.Entities.Process
{
    public class Combination
    {
        private string uniqueIdentifier;

        public Combination()
        {
            this.Numbers = new List<int>();
        }

        public int Size { get; set; }

        public List<int> Numbers { get; set; }

        public long RepeatsCount { get; set; }

        public string UniqueIdentifier
        {
            get
            {
                if (string.IsNullOrEmpty(this.uniqueIdentifier))
                {
                    this.uniqueIdentifier = this.Numbers.CalculateUniqueString();
                }
                return this.uniqueIdentifier;
            }
            set
            {
                this.uniqueIdentifier = value;
                this.Numbers = this.uniqueIdentifier.CalculateCombination();
            }
        }

        public CombinationRecord ToRecord()
        {
            return new CombinationRecord
                   {
                       UniqueIdentifier = this.UniqueIdentifier,
                       Size = this.Size
                   };
        }
    }
}
