// <copyright file="NeedlemanWunschDistance.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using IK.Algorithms.Matrices.Constants;
using IK.Algorithms.Matrices.Implementations;
using IK.Algorithms.Matrices.Interfaces;

namespace Lotto.Logic.Algorithms
{
    public enum Navigation
    {
        West,
        NorthWest,
        North
    }

    public interface IMatrixItem
    {
        int Value { get; set; }

        bool IsEmpty { get; }

        IEnumerable<Navigation> Navigations { get; }

        void AddNavigation(Navigation navigation);
    }

    public class NeedlemanWunschDistance
    {
        public string CalculateSimilarString(string source, string target)
        {
            var matrix = BuildMatrix(source, target);

            var trace = TraceToOrigin(matrix);

            string rebuiltSource;
            string rebuiltTarget;

            RebuildStrings(trace, out rebuiltSource, out rebuiltTarget);

            var sameString = rebuiltSource.Intersect(rebuiltTarget).ToString().Replace("-", "");

            return sameString;
        }

        static IMatrix<char, IMatrixItem> BuildMatrix(string source, string target)
        {
            IMatrix<char, IMatrixItem> m = new Matrix<char, IMatrixItem>();

            m.AddColumns(target.Insert(0, string.Empty).ToCharArray());
            m.AddRows(source.Insert(0, string.Empty).ToCharArray());

            var startingItems = new IMatrixItem[target.Insert(0, string.Empty).Length];

            m.Set(string.Empty[0], Dimensions.Column, startingItems);
            m.Set(string.Empty[0], Dimensions.Row, startingItems);

            for (int i = 1; i < source.Length + 1; i++)
            {
                for (int j = 1; j < target.Length + 1; j++)
                {
                    var addition = source[i] == target[j] ? 1 : -1;
                    var west = m.Get(source[i], target[j - 1]).Value + addition;
                    var north = m.Get(source[i - 1], target[j]).Value + addition;
                    var northwest = m.Get(source[i - 1], target[j - 1]).Value + addition;
                    var maxValue = new List<int> { west, north, northwest }.Max();
                    var weigthValue = m.Get(source[i], target[j]);
                    weigthValue.Value = maxValue;
                    if (maxValue == west)
                    {
                        weigthValue.AddNavigation(Navigation.West);
                    }
                    if (maxValue == north)
                    {
                        weigthValue.AddNavigation(Navigation.North);
                    }
                    if (maxValue == northwest)
                    {
                        weigthValue.AddNavigation(Navigation.NorthWest);
                    }
                    m.Set(source[i], target[j], weigthValue);
                }
            }

            return m;
        }

        static IEnumerable<IMatrixItem> TraceToOrigin(IMatrix<char, IMatrixItem> matrix)
        {
            return null;
        }

        static int RebuildStrings(IEnumerable<IMatrixItem> trace, out string rebuiltSource, out string rebuiltTarget)
        {
            rebuiltSource = string.Empty;
            rebuiltTarget = string.Empty;
            return 0;
        }
    }
}
