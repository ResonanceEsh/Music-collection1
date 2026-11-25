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
                ShowMenu();
                string choice = Console.ReadLine();
                if (choice != null) choice = choice.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": ShowSizeTable(); Pause(); break;
                    case "3": ShowListeningTable(); Pause(); break;
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

        static void ShowMenu()
        {
            Console.WriteLine("=== Музична колекція ===");
            Console.WriteLine("1 — довідка про програму");
            Console.WriteLine("2 — розмір файлу залежно від тривалості треку");
            Console.WriteLine("3 — прослуховування за жанрами й днями тижня");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
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

        static double CalculateFileSize(double minutes, double kbps)
        {
            return minutes * 60 * kbps / 8 / 1024;
        }

        static void ShowSizeTable()
        {
            Console.WriteLine("Розмір MP3-файлу залежно від тривалості треку");
            Console.WriteLine();

            double start = ReadDouble("Початок проміжку, хв (0,5–30): ", 0.5, 30);
            double finish = ReadDouble("Кінець проміжку, хв (не менше за початок, до 60): ", start, 60);
            double step = ReadDouble("Крок, хв (0,1–5): ", 0.1, 5);

            Console.WriteLine();
            TabulateFileSize(start, finish, step);
        }

        static void TabulateFileSize(double start, double finish, double step)
        {
            Console.WriteLine("{0,10}{1,14}{2,14}", "Хвилин", "128 кбіт/с", "320 кбіт/с");
            Console.WriteLine(new string('-', 38));

            for (double minutes = start; minutes <= finish + 1e-9; minutes += step)
            {
                double mb128 = CalculateFileSize(minutes, 128);
                double mb320 = CalculateFileSize(minutes, 320);
                Console.WriteLine("{0,10:F2}{1,14:F2}{2,14:F2}", minutes, mb128, mb320);
            }
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            double value;

            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (!double.TryParse(text, out value))
                {
                    Console.WriteLine("Помилка: введіть число.");
                }
                else if (!(value >= min && value <= max))
                {
                    Console.WriteLine("Помилка: число має бути від " + min + " до " + max + ".");
                }
                else
                {
                    return value;
                }
            }
        }

        static int ReadInt(string prompt, int min, int max)
        {
            int value;

            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (!int.TryParse(text, out value))
                {
                    Console.WriteLine("Помилка: введіть ціле число.");
                }
                else if (value < min || value > max)
                {
                    Console.WriteLine("Помилка: число має бути від " + min + " до " + max + ".");
                }
                else
                {
                    return value;
                }
            }
        }

        static string[] GetGenres()
        {
            return new string[] { "Рок", "Поп", "Джаз", "Класика", "Хіп-хоп", "Електроніка", "Фольк", "Блюз" };
        }

        static string[] GetDays()
        {
            return new string[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Нд" };
        }

        static int CalculateListens(int row, int col)
        {
            return (row * 7 + col * 3 + 5) % 19 + 3;
        }

        static void ShowListeningTable()
        {
            Console.WriteLine("Прослуховування за жанрами й днями тижня");
            Console.WriteLine();

            int rows = ReadInt("Скільки жанрів показати (1–" + GetGenres().Length + "): ", 1, GetGenres().Length);
            int cols = ReadInt("Скільки днів показати (1–" + GetDays().Length + "): ", 1, GetDays().Length);

            Console.WriteLine();
            PrintListeningTable(rows, cols);
        }

        static void PrintListeningTable(int rows, int cols)
        {
            string[] genres = GetGenres();
            string[] days = GetDays();

            Console.Write("{0,-12}", "Жанр");
            for (int col = 0; col < cols; col++)
            {
                Console.Write("{0,8}", days[col]);
            }
            Console.WriteLine("{0,10}", "Разом");

            int lineWidth = 12 + 8 * cols + 10;
            Console.WriteLine(new string('-', lineWidth));

            int[] colSums = new int[cols];
            int grandTotal = 0;

            for (int row = 0; row < rows; row++)
            {
                int rowSum = 0;
                Console.Write("{0,-12}", genres[row]);

                for (int col = 0; col < cols; col++)
                {
                    int value = CalculateListens(row, col);
                    Console.Write("{0,8}", value);
                    rowSum += value;
                    colSums[col] += value;
                }

                Console.WriteLine("{0,10}", rowSum);
                grandTotal += rowSum;
            }

            Console.WriteLine(new string('-', lineWidth));

            Console.Write("{0,-12}", "Разом");
            for (int col = 0; col < cols; col++)
            {
                Console.Write("{0,8}", colSums[col]);
            }
            Console.WriteLine("{0,10}", grandTotal);
        }
    }
}
