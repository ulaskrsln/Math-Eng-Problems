using System;
using System.Diagnostics;

/*

 Monte Carlo yöntemleri, rastgele örnekleme kullanarak karmaşık matematiksel ve fiziksel sistemlerin simülasyonunu ve analizini yapar.

 Bu yöntemler, özellikle analitik çözümlerin zor veya imkansız olduğu durumlarda kullanılır.

*/



namespace MonteCarloSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Monte Carlo Simülasyonu: Pi Hesaplayıcı";

            // Simülasyon parametreleri
            int totalPoints = 1_000_000; // 1 Milyon nokta (Daha hassas sonuç için artırılabilir)
            int pointsInsideCircle = 0;

            // Rastgele sayı üreteci
            Random random = new Random();

            Console.WriteLine("--- Monte Carlo Simülasyonu Başlıyor ---");
            Console.WriteLine($"Hedef: {totalPoints:N0} adet nokta fırlatmak.\n");

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 1; i <= totalPoints; i++)
            {
                // 1. Adım: [0, 1] aralığında rastgele x ve y koordinatları üret
                double x = random.NextDouble();
                double y = random.NextDouble();

                // 2. Adım: Nokta dairenin içinde mi? (Hipotenüs kontrolü: x^2 + y^2 <= 1)
                if ((x * x) + (y * y) <= 1.0)
                {
                    pointsInsideCircle++;
                }

                // Her 100.000 adımda bir durumu yazdıralım
                if (i % 100_000 == 0)
                {
                    double currentPi = 4.0 * pointsInsideCircle / i;
                    double error = Math.Abs(Math.PI - currentPi);

                    Console.WriteLine($"Atılan: {i,10:N0} | Tahmini Pi: {currentPi:F6} | Hata: {error:F6}");
                }
            }

            sw.Stop();

            double finalPi = 4.0 * pointsInsideCircle / totalPoints;

            Console.WriteLine("\n---------------- SONUÇLAR ----------------");
            Console.WriteLine($"Gerçek Pi Değeri : {Math.PI}");
            Console.WriteLine($"Monte Carlo Pi   : {finalPi}");
            Console.WriteLine($"Toplam Süre      : {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Sapma Oranı      : %{Math.Abs((Math.PI - finalPi) / Math.PI) * 100:F4}");

            Console.ReadLine();
        }
    }
}