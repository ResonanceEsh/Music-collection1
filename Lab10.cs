using System;
using System.Text;

namespace MusicCollection
{
    internal class Program
    {
        const int Capacity = 100;
        static int[] ids = new int[Capacity];
        static string[] artists = new string[Capacity];
        static string[] titles = new string[Capacity];
        static string[] albums = new string[Capacity];
        static int[] years = new int[Capacity];
        static int[] durations = new int[Capacity];
        static int[] genreIds = new int[Capacity];
        static int[] plays = new int[Capacity];
        static int count = 0;
        static int nextId = 1;

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
                    case "1": ShowAllTracks(); Pause(); break;
                    case "2": AddTrack(); Pause(); break;
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        ShowNotReady();
                        Pause();
                        break;
                    case "10": FillTestData(); Pause(); break;
                    case "11": ShowAbout(); Pause(); break;
                    case "12": ShowSizeTable(); Pause(); break;
                    case "13": ShowListeningTable(); Pause(); break;
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
            Console.WriteLine("1 — показати всі записи");
            Console.WriteLine("2 — додати запис");
            Console.WriteLine("3 — знайти за умовою");
            Console.WriteLine("4 — змінити запис");
            Console.WriteLine("5 — видалити запис");
            Console.WriteLine("6 — зведена таблиця");
            Console.WriteLine("7 — зберегти у файл");
            Console.WriteLine("8 — завантажити з файлу");
            Console.WriteLine("9 — експорт у CSV");
            Console.WriteLine("10 — заповнити тестовими даними");
            Console.WriteLine("11 — довідка про програму");
            Console.WriteLine("12 — розмір файлу залежно від тривалості треку");
            Console.WriteLine("13 — прослуховування за жанрами й днями тижня");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
        }

        static void ShowNotReady()
        {
            Console.WriteLine("Поки не зроблено.");
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void ShowAllTracks()
        {
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }

            Console.WriteLine("{0,4} {1,-18}{2,-22}{3,-18}{4,6}{5,9}{6,6}{7,11}", "№", "Виконавець", "Назва", "Альбом", "Рік", "Тривал.", "Жанр", "Прослух.");
            Console.WriteLine(new string('-', 95));

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("{0,4} {1,-18}{2,-22}{3,-18}{4,6}{5,9}{6,6}{7,11}", ids[i], artists[i], titles[i], albums[i], years[i], FormatTime(durations[i]), genreIds[i], plays[i]);
            }

            Console.WriteLine(new string('-', 95));
            Console.WriteLine("Записів: " + count + " із " + Capacity + ".");
            Console.WriteLine("Загальна тривалість колекції: " + FormatTotalTime(CalculateTotalDuration(durations, count)) + ".");
        }

        static string FormatTime(int seconds)
        {
            return (seconds / 60) + ":" + (seconds % 60).ToString("D2");
        }

        static void AddTrack()
        {
            if (count == Capacity)
            {
                Console.WriteLine("Сховище заповнене, видаліть щось зайве.");
                return;
            }

            string artist = ReadText("Виконавець (2–17 символів): ", 2, 17);
            string title = ReadText("Назва (2–21 символ): ", 2, 21);
            string album = ReadText("Альбом (2–17 символів): ", 2, 17);
            int year = ReadInt("Рік випуску (1900–" + DateTime.Now.Year + "): ", 1900, DateTime.Now.Year);
            int duration = ReadInt("Тривалість, секунд (1–7200): ", 1, 7200);
            int genreId = ReadInt("Код жанру (1–" + GetGenres().Length + "): ", 1, GetGenres().Length);
            int playCount = ReadInt("Кількість прослуховувань (0–100000000): ", 0, 100000000);

            ids[count] = nextId;
            artists[count] = artist;
            titles[count] = title;
            albums[count] = album;
            years[count] = year;
            durations[count] = duration;
            genreIds[count] = genreId;
            plays[count] = playCount;
            count++;
            nextId++;

            Console.WriteLine("Додано трек №" + ids[count - 1] + ".");
        }

        static string ReadText(string prompt, int minLength, int maxLength)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();
                if (text == null) text = "";
                text = text.Trim();

                if (text.Length >= minLength && text.Length <= maxLength)
                {
                    return text;
                }

                Console.WriteLine("Помилка: текст має містити від " + minLength + " до " + maxLength + " символів.");
            }
        }

        static int CalculateTotalDuration(int[] values, int itemCount)
        {
            int total = 0;

            for (int i = 0; i < itemCount; i++)
            {
                total += values[i];
            }

            return total;
        }

        static string FormatTotalTime(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = totalSeconds % 3600 / 60;
            int seconds = totalSeconds % 60;

            return hours + " год " + minutes + " хв " + seconds + " с";
        }

        static void FillTestData()
        {
            string[] testArtists = { "Queen", "Miles Davis", "Daft Punk", "Dua Lipa", "Eminem", "Mumford & Sons" };
            string[] testTitles = { "We Will Rock You", "So What", "One More Time", "Levitating", "Lose Yourself", "Little Lion Man" };
            string[] testAlbums = { "News of the World", "Kind of Blue", "Discovery", "Future Nostalgia", "8 Mile", "Sigh No More" };
            int[] testYears = { 1977, 1959, 2001, 2020, 2002, 2009 };
            int[] testDurations = { 122, 562, 320, 203, 326, 245 };
            int[] testGenreIds = { 1, 3, 5, 2, 4, 6 };
            int[] testPlays = { 15200, 8400, 21000, 19800, 17500, 9300 };

            int needed = testArtists.Length;

            if (count + needed > Capacity)
            {
                Console.WriteLine("Недостатньо місця для тестових даних.");
                return;
            }

            for (int i = 0; i < needed; i++)
            {
                ids[count] = nextId;
                artists[count] = testArtists[i];
                titles[count] = testTitles[i];
                albums[count] = testAlbums[i];
                years[count] = testYears[i];
                durations[count] = testDurations[i];
                genreIds[count] = testGenreIds[i];
                plays[count] = testPlays[i];
                count++;
                nextId++;
            }

            Console.WriteLine("Додано тестових записів: " + needed + ".");
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
