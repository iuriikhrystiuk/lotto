// <copyright file="CombinationRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Records.Process
{
    /// <summary>
    ///     Represents the combination record in the data base.
    /// </summary>
    public class CombinationRecord
    {
        /// <summary>
        ///     Gets or sets the unique identifier for current combination.
        /// </summary>
        public string UniqueIdentifier { get; set; }

        /// <summary>
        ///     Gets or sets the count of repetitions in overall drawing pool.
        /// </summary>
        public long RepeatsCount { get; set; }

        /// <summary>
        ///     Gets or sets the size of the combination.
        /// </summary>
        public int Size { get; set; }
    }
}