using VLStatsCodeSQL.Data;
using VLStatsCodeSQL.Models;

namespace VLStatsCodeSQL
{
    public partial class Program
    {
        private static void EfTeamsCrud(
            VlStatsDbContext context, string? operation)
        {
            switch (operation)
            {
                case "1":
                    {
                        var teams = context.Teams.ToList();
                        Console.WriteLine(
                            $"\nНайдено записей: {teams.Count}");
                        foreach (var team in teams)
                        {
                            Console.WriteLine(
                                $"  ID: {team.TeamId}, " +
                                $"Название: {team.TeamName}");
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите TeamId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var team = context.Teams.Find(id);
                        if (team == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"  ID: {team.TeamId}, " +
                                $"Название: {team.TeamName}");
                        }

                        break;
                    }
                case "3":
                    {
                        Console.Write("Название команды: ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        var team = new Team { TeamName = name };
                        context.Teams.Add(team);
                        context.SaveChanges();
                        Console.WriteLine(
                            $"Команда добавлена. ID: {team.TeamId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите TeamId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var team = context.Teams.Find(id);
                        if (team == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        Console.Write(
                            $"Новое название " +
                            $"(текущее: {team.TeamName}): ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        team.TeamName = name;
                        context.SaveChanges();
                        Console.WriteLine("Команда обновлена.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите TeamId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var team = context.Teams.Find(id);
                        if (team == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        context.Teams.Remove(team);
                        context.SaveChanges();
                        Console.WriteLine("Команда удалена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void EfPlayersCrud(
            VlStatsDbContext context, string? operation)
        {
            switch (operation)
            {
                case "1":
                    {
                        var players = context.Players.ToList();
                        Console.WriteLine(
                            $"\nНайдено записей: {players.Count}");
                        foreach (var player in players)
                        {
                            PrintPlayer(player);
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите PlayerId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var player = context.Players.Find(id);
                        if (player == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                        else
                        {
                            PrintPlayer(player);
                        }

                        break;
                    }
                case "3":
                    {
                        var player = ReadPlayerFromConsole();
                        context.Players.Add(player);
                        context.SaveChanges();
                        Console.WriteLine(
                            $"Игрок добавлен. " +
                            $"ID: {player.PlayerId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите PlayerId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var player = context.Players.Find(id);
                        if (player == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        UpdatePlayerFromConsole(player);
                        context.SaveChanges();
                        Console.WriteLine("Игрок обновлен.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите PlayerId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var player = context.Players.Find(id);
                        if (player == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        context.Players.Remove(player);
                        context.SaveChanges();
                        Console.WriteLine("Игрок удален.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void EfTournamentsCrud(
            VlStatsDbContext context, string? operation)
        {
            switch (operation)
            {
                case "1":
                    {
                        var items = context.Tournaments.ToList();
                        Console.WriteLine(
                            $"\nНайдено записей: {items.Count}");
                        foreach (var item in items)
                        {
                            Console.WriteLine(
                                $"  ID: {item.TournamentId}, " +
                                $"Название: {item.TournamentName}");
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите TournamentId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var item = context.Tournaments.Find(id);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"  ID: {item.TournamentId}, " +
                                $"Название: {item.TournamentName}");
                        }

                        break;
                    }
                case "3":
                    {
                        Console.Write("Название турнира: ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        var item = new Tournament
                        {
                            TournamentName = name
                        };
                        context.Tournaments.Add(item);
                        context.SaveChanges();
                        Console.WriteLine(
                            $"Турнир добавлен. " +
                            $"ID: {item.TournamentId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите TournamentId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var item = context.Tournaments.Find(id);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        Console.Write(
                            $"Новое название " +
                            $"(текущее: {item.TournamentName}): ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        item.TournamentName = name;
                        context.SaveChanges();
                        Console.WriteLine("Турнир обновлен.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите TournamentId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var item = context.Tournaments.Find(id);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        context.Tournaments.Remove(item);
                        context.SaveChanges();
                        Console.WriteLine("Турнир удален.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void EfMatchesCrud(
            VlStatsDbContext context, string? operation)
        {
            switch (operation)
            {
                case "1":
                    {
                        var items = context.Matches.ToList();
                        Console.WriteLine(
                            $"\nНайдено записей: {items.Count}");
                        foreach (var item in items)
                        {
                            PrintMatch(item);
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите MatchId (GUID): ");
                        string input =
                            Console.ReadLine()?.Trim() ?? "";
                        if (!Guid.TryParse(input, out var guid))
                        {
                            Console.WriteLine("Неверный формат GUID.");
                            break;
                        }

                        var item = context.Matches.Find(guid);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                        else
                        {
                            PrintMatch(item);
                        }

                        break;
                    }
                case "3":
                    {
                        var item = ReadMatchFromConsole();
                        context.Matches.Add(item);
                        context.SaveChanges();
                        Console.WriteLine(
                            $"Матч добавлен. ID: {item.MatchId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите MatchId (GUID): ");
                        string input =
                            Console.ReadLine()?.Trim() ?? "";
                        if (!Guid.TryParse(input, out var guid))
                        {
                            Console.WriteLine("Неверный формат GUID.");
                            break;
                        }

                        var item = context.Matches.Find(guid);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        UpdateMatchFromConsole(item);
                        context.SaveChanges();
                        Console.WriteLine("Матч обновлен.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите MatchId (GUID): ");
                        string input =
                            Console.ReadLine()?.Trim() ?? "";
                        if (!Guid.TryParse(input, out var guid))
                        {
                            Console.WriteLine("Неверный формат GUID.");
                            break;
                        }

                        var item = context.Matches.Find(guid);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        context.Matches.Remove(item);
                        context.SaveChanges();
                        Console.WriteLine("Матч удален.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void EfPlayerMatchStatsCrud(
            VlStatsDbContext context, string? operation)
        {
            switch (operation)
            {
                case "1":
                    {
                        var items =
                            context.PlayerMatchStats.ToList();
                        Console.WriteLine(
                            $"\nНайдено записей: {items.Count}");
                        foreach (var item in items)
                        {
                            PrintPlayerMatchStat(item);
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите PlayerId: ");
                        int playerId = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        Console.Write("Введите MatchId (GUID): ");
                        string input =
                            Console.ReadLine()?.Trim() ?? "";
                        if (!Guid.TryParse(input, out var matchId))
                        {
                            Console.WriteLine("Неверный формат GUID.");
                            break;
                        }

                        var item = context.PlayerMatchStats
                            .Find(playerId, matchId);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                        else
                        {
                            PrintPlayerMatchStat(item);
                        }

                        break;
                    }
                case "3":
                    {
                        var item = ReadPlayerMatchStatFromConsole();
                        context.PlayerMatchStats.Add(item);
                        context.SaveChanges();
                        Console.WriteLine("Статистика добавлена.");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите PlayerId: ");
                        int playerId = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        Console.Write("Введите MatchId (GUID): ");
                        string input =
                            Console.ReadLine()?.Trim() ?? "";
                        if (!Guid.TryParse(input, out var matchId))
                        {
                            Console.WriteLine("Неверный формат GUID.");
                            break;
                        }

                        var item = context.PlayerMatchStats
                            .Find(playerId, matchId);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        UpdatePlayerMatchStatFromConsole(item);
                        context.SaveChanges();
                        Console.WriteLine("Статистика обновлена.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите PlayerId: ");
                        int playerId = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        Console.Write("Введите MatchId (GUID): ");
                        string input =
                            Console.ReadLine()?.Trim() ?? "";
                        if (!Guid.TryParse(input, out var matchId))
                        {
                            Console.WriteLine("Неверный формат GUID.");
                            break;
                        }

                        var item = context.PlayerMatchStats
                            .Find(playerId, matchId);
                        if (item == null)
                        {
                            Console.WriteLine("Запись не найдена.");
                            break;
                        }

                        context.PlayerMatchStats.Remove(item);
                        context.SaveChanges();
                        Console.WriteLine("Статистика удалена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }
    }
}
