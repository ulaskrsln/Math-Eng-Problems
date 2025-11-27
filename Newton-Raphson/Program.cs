using System;

// Bir eğrinin üzerindeki bir noktadan teğet (türev) çizeriz ve bu teğetin x eksenini kestiği noktayı "yeni tahminimiz" olarak kabul ederiz.

namespace NewtonRaphsonSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Newton-Raphson Kök Bulma";

            // Çözülecek Fonksiyon: f(x) = x^3 - 2x - 5
            // Hedef: Bu fonksiyonu 0 yapan x değerini bulmak.
            // (Analitik olarak kök yaklaşık 2.09455 civarındadır)
            Func<double, double> function = x => Math.Pow(x, 3) - (2 * x) - 5;

            // Başlangıç tahmini (Rastgele bir nokta)
            double initialGuess = 2.0;

            Console.WriteLine("--- Newton-Raphson Metodu Başlatılıyor ---");
            Console.WriteLine($"Fonksiyon: x^3 - 2x - 5 = 0");
            Console.WriteLine($"Başlangıç Tahmini: {initialGuess}\n");

            SolveNewtonRaphson(function, initialGuess);

            Console.ReadLine();
        }

        static void SolveNewtonRaphson(Func<double, double> f, double x0)
        {
            double x = x0;
            double tolerance = 1e-7; // 0.0000001 hassasiyet
            int maxIter = 20;

            Console.WriteLine("Iter |      x (Tahmin)      |     f(x) Değeri      |    f'(x) (Eğim)      |   Hata");
            Console.WriteLine(new string('-', 90));

            for (int i = 1; i <= maxIter; i++)
            {
                double y = f(x);

                double derivative = CalculateNumericalDerivative(f, x);

                // Türev 0 ise bölme hatası olur (Teğet x eksenine paraleldir)
                if (Math.Abs(derivative) < 1e-10)
                {
                    Console.WriteLine("Hata: Türev 0'a çok yakın. Yerel minimum/maksimum noktasına takıldık.");
                    break;
                }

                // Newton-Raphson Formülü: x_yeni = x_eski - f(x)/f'(x)
                double xNew = x - (y / derivative);

                double error = Math.Abs(xNew - x);

                Console.WriteLine($"{i,4} | {x,20:F8} | {y,20:F8} | {derivative,20:F8} | {error,10:E4}");

                if (error < tolerance) //yakınsama kontrolü
                {
                    Console.WriteLine(new string('-', 90));
                    Console.WriteLine($"🎉 Kök Bulundu: {xNew:F8}");
                    Console.WriteLine($"Toplam İterasyon: {i}");
                    return;
                }

                x = xNew;
            }

            Console.WriteLine("Maksimum iterasyon sayısına ulaşıldı.");
        }

        static double CalculateNumericalDerivative(Func<double, double> f, double x)
        {
            double h = 1e-5; // Küçük bir adım aralığı
            return (f(x + h) - f(x - h)) / (2 * h);
        }
    }
}