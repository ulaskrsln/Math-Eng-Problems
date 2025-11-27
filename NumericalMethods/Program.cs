using System;
using System.Collections.Generic;

/*RK4, bir eğrinin nereye kıvrılacağını, daha kıvrılmadan önce 4 farklı senaryoyla tahmin eden bir algoritmadır.
 Euler metoduna kıyasla çok daha hassas tahminler yaparak diferansiyel denklemin değerini yüksek doğrulukta hesaplar.*/




namespace NumericalMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Runge-Kutta 4 (RK4) Solver";

            Func<double, double, double> function = (x, y) => x + y;

            double x0 = 0;      // Başlangıç x
            double y0 = 1;      // Başlangıç y (Initial Condition)
            double xEnd = 1.0;  // Nereye kadar hesaplayacağız?
            double h = 0.1;     // Adım büyüklüğü (Step size)

            Console.WriteLine($"Denklem: dy/dx = x + y");
            Console.WriteLine($"Başlangıç: y({x0}) = {y0}, Adım Aralığı (h): {h}\n");

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Adım (x) | RK4 Tahmini (y) | Gerçek Değer (Analitik) | Hata");
            Console.WriteLine("-------------------------------------------------------");

            SolveRK4(function, x0, y0, xEnd, h);

            Console.ReadLine();
        }

        static void SolveRK4(Func<double, double, double> f, double x0, double y0, double xEnd, double h)
        {
            double x = x0;
            double y = y0;

            PrintStep(x, y);

            while (x < xEnd)
            {
                double k1 = h * f(x, y);
                double k2 = h * f(x + h / 2.0, y + k1 / 2.0);
                double k3 = h * f(x + h / 2.0, y + k2 / 2.0);
                double k4 = h * f(x + h, y + k3);

                y = y + (1.0 / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);

                x += h;

                PrintStep(x, y);
            }
        }

        static void PrintStep(double x, double y)
        {
            double exact = 2 * Math.Exp(x) - x - 1;
            double error = Math.Abs(exact - y);

            Console.WriteLine($"{x,8:F2} | {y,15:F6} | {exact,23:F6} | {error,10:E4}");
        }
    }
}