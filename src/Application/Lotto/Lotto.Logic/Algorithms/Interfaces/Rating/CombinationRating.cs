using Lotto.Model.Entities.Process;

namespace Lotto.Logic.Algorithms.Interfaces.Rating
{
    public class CombinationRating
    {
        public Combination Combination { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Rating { get; set; }
    }
}
