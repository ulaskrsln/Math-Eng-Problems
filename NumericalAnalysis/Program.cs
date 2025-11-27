using System;
using System.Linq;

namespace NumericalAnalysisSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Gauss-Seidel Çözücü";

            // ÖRNEK SİSTEM (Köşegen Baskın Bir Matris Seçtik)
            // 4x -  y +  z = 12
            // -x + 4y - 2z = -1
            //  x - 2y + 4z = 5

            double[,] A = {
                {  4, -1,  1 },
                { -1,  4, -2 },
                {  1, -2,  4 }
            };

            double[] b = { 12, -1, 5 };

            double[] initialGuess = { 0, 0, 0 };

            // Hata toleransı ve maksimum iterasyon
            double tolerance = 0.0001;
            int maxIterations = 50;

            Console.WriteLine("--- Gauss-Seidel Yöntemi Başlatılıyor ---");

            if (IsDiagonallyDominant(A))
            {
                Console.WriteLine("Matris Köşegen Baskın (Diagonally Dominant). Yakınsama garantili.");
            }
            else
            {
                Console.WriteLine("Matris Köşegen Baskın DEĞİL. Yakınsamayabilir!");
            }
            Console.WriteLine("-----------------------------------------");

            SolveGaussSeidel(A, b, initialGuess, tolerance, maxIterations);

            Console.ReadLine();
        }

        static void SolveGaussSeidel(double[,] A, double[] b, double[] x, double tol, int maxIter)
        {
            int n = b.Length;

            Console.Write("Iter |");
            for (int i = 0; i < n; i++) Console.Write($"   x{i + 1}      |");
            Console.WriteLine("   Hata (Error)");
            Console.WriteLine(new string('-', 50));

            for (int k = 1; k <= maxIter; k++)
            {
                double[] xOld = (double[])x.Clone(); // Hata hesabı için eski değerler

                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            sum += A[i, j] * x[j];
                        }
                    }
                    x[i] = (b[i] - sum) / A[i, i];
                }

                // Hata Hesabı (Öklid Normu veya Maksimum Fark kullanılabilir)
                // Burada |x_yeni - x_eski|'nin en büyüğüne bakıyoruz.
                double error = 0;
                for (int i = 0; i < n; i++)
                {
                    double diff = Math.Abs(x[i] - xOld[i]);
                    if (diff > error) error = diff;
                }

                Console.Write($"{k,4} |");
                for (int i = 0; i < n; i++) Console.Write($"{x[i],10:F5} |");
                Console.WriteLine($"{error,12:F6}");

                if (error < tol)
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine($" Çözüm {k}. iterasyonda bulundu!");
                    return;
                }
            }

            Console.WriteLine("Maksimum iterasyon sayısına ulaşıldı. Çözüm yakınsamadı.");
        }

        static bool IsDiagonallyDominant(double[,] matrix)
        {
            int rows = matrix.GetLength(0);

            for (int i = 0; i < rows; i++)
            {
                double diagonal = Math.Abs(matrix[i, i]);
                double sumOthers = 0;

                for (int j = 0; j < rows; j++)
                {
                    if (i != j)
                    {
                        sumOthers += Math.Abs(matrix[i, j]);
                    }
                }

                if (diagonal < sumOthers) return false;
            }
            return true;
        }
    }
}