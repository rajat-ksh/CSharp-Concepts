using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix matrixA, IMatrix matrixB)
        {
            var result = new Matrix(matrixA.RowCount, matrixB.ColCount);

            long matACols = matrixA.ColCount;
            long matBCols = matrixB.ColCount;
            long matARows = matrixA.RowCount;

            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    long temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matrixA.GetElement(i, k) * matrixB.GetElement(k, j);
                    }
                    result.SetElement(i, j, temp);
                }
            });

            return result;
        }
    }
}
