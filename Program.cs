using Microsoft.Extensions.Configuration;
using VLStatsCodeSQL.Data;

namespace VLStatsCodeSQL
{
    public partial class Program
    {
        private static string s_connectionString = string.Empty;

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            s_connectionString = configuration
                .GetConnectionString("VlStats")
                ?? throw new InvalidOperationException(
                    "Строка подключения 'VlStats' не найдена.");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== VLSTATS CRUD Утилита ===");
            Console.WriteLine(
                $"Подключение: {s_connectionString}");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("--- Главное меню ---");
                Console.WriteLine("1. ADO.NET");
                Console.WriteLine("2. Entity Framework");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите подход: ");

                var approach = Console.ReadLine()?.Trim();

                switch (approach)
                {
                    case "1":
                        ShowTableMenu(useEf: false);
                        break;
                    case "2":
                        ShowTableMenu(useEf: true);
                        break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        private static void ShowTableMenu(bool useEf)
        {
            string mode = useEf
                ? "Entity Framework"
                : "ADO.NET";

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"--- Выбор таблицы ({mode}) ---");
                Console.WriteLine("1. Teams");
                Console.WriteLine("2. Players");
                Console.WriteLine("3. Tournaments");
                Console.WriteLine("4. Matches");
                Console.WriteLine("5. PlayerMatchStats");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите таблицу: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        ShowCrudMenu("Teams", useEf);
                        break;
                    case "2":
                        ShowCrudMenu("Players", useEf);
                        break;
                    case "3":
                        ShowCrudMenu("Tournaments", useEf);
                        break;
                    case "4":
                        ShowCrudMenu("Matches", useEf);
                        break;
                    case "5":
                        ShowCrudMenu("PlayerMatchStats", useEf);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        private static void ShowCrudMenu(
            string tableName, bool useEf)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"--- {tableName} " +
                    $"({(useEf ? "EF" : "ADO.NET")}) ---");
                Console.WriteLine("1. Показать все (Read)");
                Console.WriteLine("2. Показать по ID (Read)");
                Console.WriteLine("3. Добавить (Create)");
                Console.WriteLine("4. Обновить (Update)");
                Console.WriteLine("5. Удалить (Delete)");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите операцию: ");

                var choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                {
                    return;
                }

                try
                {
                    if (useEf)
                    {
                        ExecuteEf(tableName, choice);
                    }
                    else
                    {
                        ExecuteAdo(tableName, choice);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private static void ExecuteEf(
            string tableName, string? operation)
        {
            using var context = new VlStatsDbContext(
                s_connectionString);

            switch (tableName)
            {
                case "Teams":
                    EfTeamsCrud(context, operation);
                    break;
                case "Players":
                    EfPlayersCrud(context, operation);
                    break;
                case "Tournaments":
                    EfTournamentsCrud(context, operation);
                    break;
                case "Matches":
                    EfMatchesCrud(context, operation);
                    break;
                case "PlayerMatchStats":
                    EfPlayerMatchStatsCrud(context, operation);
                    break;
            }
        }

        private static void ExecuteAdo(
            string tableName, string? operation)
        {
            switch (tableName)
            {
                case "Teams":
                    AdoTeamsCrud(operation);
                    break;
                case "Players":
                    AdoPlayersCrud(operation);
                    break;
                case "Tournaments":
                    AdoTournamentsCrud(operation);
                    break;
                case "Matches":
                    AdoMatchesCrud(operation);
                    break;
                case "PlayerMatchStats":
                    AdoPlayerMatchStatsCrud(operation);
                    break;
            }
        }
    }
}
