using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Theory Zero-Sum Game";

            int[,] payoffMatrix = {
                { 2,  4,  1 },
                { 4,  2,  1 }, 
                { -2, 1,  -5 }
            };
            Console.WriteLine("--- Ödeme Matrisi ---");
            PrintMatrix(payoffMatrix);
            Console.WriteLine("---------------------");

            SolveZeroSumGame(payoffMatrix);

            Console.ReadLine();
        }

        static void SolveZeroSumGame(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[] rowMins = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                int minVal = int.MaxValue;
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < minVal)
                        minVal = matrix[i, j];
                }
                rowMins[i] = minVal;
            }

            int[] colMaxs = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                int maxVal = int.MinValue;
                for (int i = 0; i < rows; i++)
                {
                    if (matrix[i, j] > maxVal)
                        maxVal = matrix[i, j];
                }
                colMaxs[j] = maxVal;
            }

            int maximin = rowMins.Max(); // Satır minimumlarının en büyüğü
            int minimax = colMaxs.Min(); // Sütun maksimumlarının en küçüğü

            Console.WriteLine($"Maximin (Satır oyuncusunun garantilediği): {maximin}");
            Console.WriteLine($"Minimax (Sütun oyuncusunun sınırladığı): {minimax}");
            Console.WriteLine("---------------------");

            if (maximin == minimax)
            {
                Console.WriteLine($" DENGE NOKTASI BULUNDU (Semer Noktası)!");
                Console.WriteLine($"Oyunun Değeri (V): {maximin}");

                FindOptimalStrategies(matrix, maximin);
            }
            else
            {
                Console.WriteLine("X Semer Noktası Yok.");
                Console.WriteLine("Bu oyun 'Karma Stratejiler' (Mixed Strategies) gerektirir.");
                Console.WriteLine($"Oyunun değeri {maximin} ile {minimax} arasındadır.");
            }
        }

        static void FindOptimalStrategies(int[,] matrix, int saddleValue)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    bool isRowMin = true;
                    bool isColMax = true;

                    for (int c = 0; c < cols; c++)
                        if (matrix[i, c] < matrix[i, j]) isRowMin = false;

                    for (int r = 0; r < rows; r++)
                        if (matrix[r, j] > matrix[i, j]) isColMax = false;

                    if (isRowMin && isColMax && matrix[i, j] == saddleValue)
                    {
                        Console.WriteLine($"Optimal Strateji: Satır {i + 1}, Sütun {j + 1}");
                    }
                }
            }
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(string.Format("{0,5}", matrix[i, j]));
                }
                Console.WriteLine();
            }
        }
    }
}
