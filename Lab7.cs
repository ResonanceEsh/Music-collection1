using System;
using System.Text;

namespace MusicCollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Музична колекція ===");
                Console.WriteLine("1 — довідка про програму");
                Console.WriteLine("2 — розмір файлу залежно від тривалості треку");
                Console.WriteLine("3 — прослуховування за жанрами й днями тижня");
                Console.WriteLine("0 — вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                if (choice != null) choice = choice.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "3":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("До побачення.");
                        break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void ShowAbout()
        {
            Console.WriteLine("Тема: Музична колекція.");
            Console.WriteLine("Облік музичних треків.");
            Console.WriteLine("Сутності: Користувач, Трек, Жанр (довідник).");
            Console.WriteLine("Головна сутність: Трек. Полів: 8.");
            Console.WriteLine();
            Console.WriteLine("Поля сутності \"Трек\":");
            Console.WriteLine("{0,-4}{1,-30}{2}", "№", "Поле", "Тип");
            Console.WriteLine("{0,-4}{1,-30}{2}", "1", "код", "int");
            Console.WriteLine("{0,-4}{1,-30}{2}", "2", "виконавець", "string");
            Console.WriteLine("{0,-4}{1,-30}{2}", "3", "назва", "string");
            Console.WriteLine("{0,-4}{1,-30}{2}", "4", "альбом", "string");
            Console.WriteLine("{0,-4}{1,-30}{2}", "5", "рік", "int");
            Console.WriteLine("{0,-4}{1,-30}{2}", "6", "тривалість у секундах", "int");
            Console.WriteLine("{0,-4}{1,-30}{2}", "7", "код жанру (з довідника)", "int");
            Console.WriteLine("{0,-4}{1,-30}{2}", "8", "кількість прослуховувань", "int");
            Console.WriteLine();
            Console.WriteLine("Підсумкова величина: загальна тривалість колекції");
            Console.WriteLine("та топ-5 треків за прослуховуваннями.");
        }
    }
}
