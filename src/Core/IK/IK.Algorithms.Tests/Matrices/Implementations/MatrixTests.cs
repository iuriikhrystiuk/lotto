using System.Linq;
using IK.Algorithms.Matrices.Constants;
using IK.Algorithms.Matrices.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IK.Algorithms.Tests.Matrices.Implementations
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Multiplication_MultipliesMatricesCorrectly()
        {
            var source = new Matrix<int, int>();
            source.AddRows(Enumerable.Range(1, 10).ToArray());
            source.AddColumns(Enumerable.Range(1, 5).ToArray());
            for (int i = 0; i < source.Rows.Count(); i++)
            {
                source.Set(source.Rows.ElementAt(i), Dimensions.Row, Enumerable.Range(10, 5).ToArray());
            }

            var target = new Matrix<int, int>();
            target.AddRows(Enumerable.Range(1, 5).ToArray());
            target.AddColumns(Enumerable.Range(1, 10).ToArray());
            for (int i = 0; i < target.Rows.Count(); i++)
            {
                target.Set(target.Rows.ElementAt(i), Dimensions.Row, Enumerable.Range(10, 10).ToArray());
            }

            var result = source.MultiplyBy(target, (row, column) => row * column, (sum, element) => sum + element);

            var index = 0;
            foreach (var row in result.Rows)
            {
                var resultRow = result.Get(row, Dimensions.Row);
                Assert.IsTrue(resultRow.All(i => i == 600 + index * 60));
                index++;
            }
        }

        [TestMethod]
        public void Transponse_TransponsesMatrixCorrectly()
        {
            var source = new Matrix<int, int>();
            source.AddRows(Enumerable.Range(1, 10).ToArray());
            source.AddColumns(Enumerable.Range(1, 5).ToArray());
            for (int i = 0; i < source.Rows.Count(); i++)
            {
                source.Set(source.Rows.ElementAt(i), Dimensions.Row, Enumerable.Range(10, 5).ToArray());
            }

            source.Transponse();

            var index = 0;
            foreach (var row in source.Rows)
            {
                var resultRow = source.Get(row, Dimensions.Row);
                Assert.IsTrue(resultRow.All(i => i == 10 + index));
                index++;
            }
        }
    }
}
