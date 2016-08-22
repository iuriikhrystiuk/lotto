// <copyright file="SimpleRatingCalculator.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using IK.Algorithms.Matrices.Extensions;
using IK.Algorithms.Matrices.Implementations;
using IK.Algorithms.Matrices.Interfaces;
using Lotto.Logic.Algorithms.Interfaces;
using Lotto.Logic.Algorithms.Interfaces.Rating;
using Lotto.Model.Entities.Process;
using Lotto.Model.Extensions;

namespace Lotto.Logic.Algorithms.Implementation.Rating
{
    internal class SimpleRatingCalculator : IRatingCalculator
    {
        private readonly IComparer<IList<int>> comparer;

        public SimpleRatingCalculator(IComparer<IList<int>> comparer)
        {
            this.comparer = comparer;
        }

        public IEnumerable<CombinationRating> CalculateRating(IList<Combination> combinationsToUse, int minRequiredOccurances, int size)
        {
            List<CombinationRating> inputCombinations = new List<CombinationRating>(combinationsToUse.Select(c => new CombinationRating { Combination = c, Rating = 1 }));
            if (size > minRequiredOccurances)
            {
                Matrix<string, IList<int>> matrix = new Matrix<string, IList<int>>();
                matrix.AddRows(inputCombinations.Select(c => c.Combination.UniqueIdentifier).ToArray());
                matrix.AddColumns(inputCombinations.Select(c => c.Combination.UniqueIdentifier).ToArray());

                foreach (CombinationRating row in inputCombinations)
                {
                    foreach (CombinationRating column in inputCombinations)
                    {
                        if (row.Combination.UniqueIdentifier == column.Combination.UniqueIdentifier)
                        {
                            matrix.Set(row.Combination.UniqueIdentifier, column.Combination.UniqueIdentifier, new List<int>());
                        }
                        else
                        {
                            IList<int> existingValue = matrix.Get(column.Combination.UniqueIdentifier, row.Combination.UniqueIdentifier);
                            if (existingValue == null)
                            {
                                List<int> sameNumbers = row.Combination.Numbers.Intersect(column.Combination.Numbers).ToList();
                                matrix.Set(row.Combination.UniqueIdentifier, column.Combination.UniqueIdentifier, sameNumbers);
                            }
                        }
                    }
                }

                List<IOccurrence<string, IList<int>>> occurences = matrix.CalculateOccurrences(this.comparer).ToList();
                List<Combination> occuranceRatings = occurences
                    .Where(m => m.Value.Count >= minRequiredOccurances)
                    .Select(mi => new Combination { Numbers = mi.Value.ToList(), RepeatsCount = mi.Repeats })
                    .ToList();
                int minimumOccuranceSize = occuranceRatings.Any() ? occuranceRatings.Min(c => c.Numbers.Count) : 0;
                List<CombinationRating> ratings = this.CalculateRating(occuranceRatings, minRequiredOccurances, minimumOccuranceSize).ToList();
                IEnumerable<IOccurrence<string, IList<int>>> filteredOccurances =
                    occurences.Where(
                        m => ratings.Any(r => r.Combination.UniqueIdentifier == m.Value.CalculateUniqueString()));
                foreach (IOccurrence<string, IList<int>> occurance in filteredOccurances)
                {
                    CombinationRating occurancerating =
                        ratings.First(r => r.Combination.UniqueIdentifier == occurance.Value.CalculateUniqueString());
                    foreach (IMatrixItem<string, IList<int>> matrixItem in occurance.MatrixItems)
                    {
                        CombinationRating combination = inputCombinations.First(i => i.Combination.UniqueIdentifier == matrixItem.Column);
                        combination.Rating += occurancerating.Rating;
                        combination = inputCombinations.First(i => i.Combination.UniqueIdentifier == matrixItem.Row);
                        combination.Rating += occurancerating.Rating;
                    }
                }
            }

            return inputCombinations;
        }
    }
}
