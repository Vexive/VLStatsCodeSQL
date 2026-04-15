using Microsoft.Data.SqlClient;

namespace VLStatsCodeSQL
{
    public partial class Program
    {

        private static void AdoTeamsCrud(string? operation)
        {
            using var connection =
                new SqlConnection(s_connectionString);
            connection.Open();

            switch (operation)
            {
                case "1":
                    {
                        using var command = new SqlCommand(
                            "SELECT TeamId, TeamName FROM Teams",
                            connection);
                        using var reader = command.ExecuteReader();
                        Console.WriteLine("\nСписок команд:");
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"  ID: {reader.GetInt32(0)}, " +
                                $"Название: {reader.GetString(1)}");
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите TeamId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        using var command = new SqlCommand(
                            "SELECT TeamId, TeamName " +
                            "FROM Teams WHERE TeamId = @Id",
                            connection);
                        command.Parameters.AddWithValue("@Id", id);
                        using var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            Console.WriteLine(
                                $"  ID: {reader.GetInt32(0)}, " +
                                $"Название: {reader.GetString(1)}");
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }

                        break;
                    }
                case "3":
                    {
                        Console.Write("Название команды: ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        using var command = new SqlCommand(
                            "INSERT INTO Teams (TeamName) " +
                            "OUTPUT INSERTED.TeamId " +
                            "VALUES (@Name)",
                            connection);
                        command.Parameters
                            .AddWithValue("@Name", name);
                        int newId = (int)command.ExecuteScalar();
                        Console.WriteLine(
                            $"Команда добавлена. ID: {newId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите TeamId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        Console.Write("Новое название: ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        using var command = new SqlCommand(
                            "UPDATE Teams " +
                            "SET TeamName = @Name " +
                            "WHERE TeamId = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", id);
                        command.Parameters
                            .AddWithValue("@Name", name);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Команда обновлена."
                            : "Запись не найдена.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите TeamId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        using var command = new SqlCommand(
                            "DELETE FROM Teams WHERE TeamId = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", id);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Команда удалена."
                            : "Запись не найдена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void AdoPlayersCrud(string? operation)
        {
            using var connection =
                new SqlConnection(s_connectionString);
            connection.Open();

            switch (operation)
            {
                case "1":
                    {
                        using var command = new SqlCommand(
                            "SELECT PlayerID, FirstName, LastName, " +
                            "JerseyNumber, SpikeHeight, " +
                            "IsRightHanded, TeamID FROM Players",
                            connection);
                        using var reader = command.ExecuteReader();
                        Console.WriteLine("\nСписок игроков:");
                        while (reader.Read())
                        {
                            PrintPlayerFromReader(reader);
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите PlayerId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        using var command = new SqlCommand(
                            "SELECT PlayerID, FirstName, LastName, " +
                            "JerseyNumber, SpikeHeight, " +
                            "IsRightHanded, TeamID " +
                            "FROM Players WHERE PlayerID = @Id",
                            connection);
                        command.Parameters.AddWithValue("@Id", id);
                        using var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            PrintPlayerFromReader(reader);
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }

                        break;
                    }
                case "3":
                    {
                        var player = ReadPlayerFromConsole();
                        using var command = new SqlCommand(
                            "INSERT INTO Players " +
                            "(FirstName, LastName, JerseyNumber, " +
                            "SpikeHeight, IsRightHanded, TeamID) " +
                            "OUTPUT INSERTED.PlayerID " +
                            "VALUES (@FirstName, @LastName, " +
                            "@JerseyNumber, @SpikeHeight, " +
                            "@IsRightHanded, @TeamID)",
                            connection);
                        command.Parameters.AddWithValue(
                            "@FirstName", player.FirstName);
                        command.Parameters.AddWithValue(
                            "@LastName", player.LastName);
                        command.Parameters.AddWithValue(
                            "@JerseyNumber", player.JerseyNumber);
                        command.Parameters.AddWithValue(
                            "@SpikeHeight",
                            (object?)player.SpikeHeight
                            ?? DBNull.Value);
                        command.Parameters.AddWithValue(
                            "@IsRightHanded", player.IsRightHanded);
                        command.Parameters.AddWithValue(
                            "@TeamID",
                            (object?)player.TeamId
                            ?? DBNull.Value);
                        int newId = (int)command.ExecuteScalar();
                        Console.WriteLine(
                            $"Игрок добавлен. ID: {newId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите PlayerId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        var player = ReadPlayerFromConsole();
                        using var command = new SqlCommand(
                            "UPDATE Players SET " +
                            "FirstName = @FirstName, " +
                            "LastName = @LastName, " +
                            "JerseyNumber = @JerseyNumber, " +
                            "SpikeHeight = @SpikeHeight, " +
                            "IsRightHanded = @IsRightHanded, " +
                            "TeamID = @TeamID " +
                            "WHERE PlayerID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", id);
                        command.Parameters.AddWithValue(
                            "@FirstName", player.FirstName);
                        command.Parameters.AddWithValue(
                            "@LastName", player.LastName);
                        command.Parameters.AddWithValue(
                            "@JerseyNumber", player.JerseyNumber);
                        command.Parameters.AddWithValue(
                            "@SpikeHeight",
                            (object?)player.SpikeHeight
                            ?? DBNull.Value);
                        command.Parameters.AddWithValue(
                            "@IsRightHanded", player.IsRightHanded);
                        command.Parameters.AddWithValue(
                            "@TeamID",
                            (object?)player.TeamId
                            ?? DBNull.Value);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Игрок обновлен."
                            : "Запись не найдена.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите PlayerId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        using var command = new SqlCommand(
                            "DELETE FROM Players " +
                            "WHERE PlayerID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", id);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Игрок удален."
                            : "Запись не найдена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void AdoTournamentsCrud(
            string? operation)
        {
            using var connection =
                new SqlConnection(s_connectionString);
            connection.Open();

            switch (operation)
            {
                case "1":
                    {
                        using var command = new SqlCommand(
                            "SELECT TournamentID, TournamentName " +
                            "FROM Tournaments",
                            connection);
                        using var reader = command.ExecuteReader();
                        Console.WriteLine("\nСписок турниров:");
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"  ID: {reader.GetInt32(0)}, " +
                                $"Название: {reader.GetString(1)}");
                        }

                        break;
                    }
                case "2":
                    {
                        Console.Write("Введите TournamentId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        using var command = new SqlCommand(
                            "SELECT TournamentID, TournamentName " +
                            "FROM Tournaments " +
                            "WHERE TournamentID = @Id",
                            connection);
                        command.Parameters.AddWithValue("@Id", id);
                        using var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            Console.WriteLine(
                                $"  ID: {reader.GetInt32(0)}, " +
                                $"Название: {reader.GetString(1)}");
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }

                        break;
                    }
                case "3":
                    {
                        Console.Write("Название турнира: ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        using var command = new SqlCommand(
                            "INSERT INTO Tournaments " +
                            "(TournamentName) " +
                            "OUTPUT INSERTED.TournamentID " +
                            "VALUES (@Name)",
                            connection);
                        command.Parameters
                            .AddWithValue("@Name", name);
                        int newId = (int)command.ExecuteScalar();
                        Console.WriteLine(
                            $"Турнир добавлен. ID: {newId}");
                        break;
                    }
                case "4":
                    {
                        Console.Write("Введите TournamentId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        Console.Write("Новое название: ");
                        string name =
                            Console.ReadLine()?.Trim() ?? "";
                        using var command = new SqlCommand(
                            "UPDATE Tournaments " +
                            "SET TournamentName = @Name " +
                            "WHERE TournamentID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", id);
                        command.Parameters
                            .AddWithValue("@Name", name);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Турнир обновлен."
                            : "Запись не найдена.");
                        break;
                    }
                case "5":
                    {
                        Console.Write("Введите TournamentId: ");
                        int id = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        using var command = new SqlCommand(
                            "DELETE FROM Tournaments " +
                            "WHERE TournamentID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", id);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Турнир удален."
                            : "Запись не найдена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void AdoMatchesCrud(string? operation)
        {
            using var connection =
                new SqlConnection(s_connectionString);
            connection.Open();

            switch (operation)
            {
                case "1":
                    {
                        using var command = new SqlCommand(
                            "SELECT MatchID, MatchDate, " +
                            "TournamentID, HomeTeamID, AwayTeamID " +
                            "FROM Matches",
                            connection);
                        using var reader = command.ExecuteReader();
                        Console.WriteLine("\nСписок матчей:");
                        while (reader.Read())
                        {
                            PrintMatchFromReader(reader);
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

                        using var command = new SqlCommand(
                            "SELECT MatchID, MatchDate, " +
                            "TournamentID, HomeTeamID, AwayTeamID " +
                            "FROM Matches WHERE MatchID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", guid);
                        using var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            PrintMatchFromReader(reader);
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }

                        break;
                    }
                case "3":
                    {
                        var match = ReadMatchFromConsole();
                        using var command = new SqlCommand(
                            "INSERT INTO Matches " +
                            "(MatchDate, TournamentID, " +
                            "HomeTeamID, AwayTeamID) " +
                            "OUTPUT INSERTED.MatchID " +
                            "VALUES (@MatchDate, @TournamentID, " +
                            "@HomeTeamID, @AwayTeamID)",
                            connection);
                        command.Parameters.AddWithValue(
                            "@MatchDate", match.MatchDate);
                        command.Parameters.AddWithValue(
                            "@TournamentID",
                            (object?)match.TournamentId
                            ?? DBNull.Value);
                        command.Parameters.AddWithValue(
                            "@HomeTeamID",
                            (object?)match.HomeTeamId
                            ?? DBNull.Value);
                        command.Parameters.AddWithValue(
                            "@AwayTeamID",
                            (object?)match.AwayTeamId
                            ?? DBNull.Value);
                        var newId = (Guid)command.ExecuteScalar();
                        Console.WriteLine(
                            $"Матч добавлен. ID: {newId}");
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

                        var match = ReadMatchFromConsole();
                        using var command = new SqlCommand(
                            "UPDATE Matches SET " +
                            "MatchDate = @MatchDate, " +
                            "TournamentID = @TournamentID, " +
                            "HomeTeamID = @HomeTeamID, " +
                            "AwayTeamID = @AwayTeamID " +
                            "WHERE MatchID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", guid);
                        command.Parameters.AddWithValue(
                            "@MatchDate", match.MatchDate);
                        command.Parameters.AddWithValue(
                            "@TournamentID",
                            (object?)match.TournamentId
                            ?? DBNull.Value);
                        command.Parameters.AddWithValue(
                            "@HomeTeamID",
                            (object?)match.HomeTeamId
                            ?? DBNull.Value);
                        command.Parameters.AddWithValue(
                            "@AwayTeamID",
                            (object?)match.AwayTeamId
                            ?? DBNull.Value);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Матч обновлен."
                            : "Запись не найдена.");
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

                        using var command = new SqlCommand(
                            "DELETE FROM Matches " +
                            "WHERE MatchID = @Id",
                            connection);
                        command.Parameters
                            .AddWithValue("@Id", guid);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Матч удален."
                            : "Запись не найдена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }

        private static void AdoPlayerMatchStatsCrud(
            string? operation)
        {
            using var connection =
                new SqlConnection(s_connectionString);
            connection.Open();

            switch (operation)
            {
                case "1":
                    {
                        using var command = new SqlCommand(
                            "SELECT PlayerID, MatchID, " +
                            "PointsScored, IsStarter " +
                            "FROM PlayerMatchStats",
                            connection);
                        using var reader = command.ExecuteReader();
                        Console.WriteLine(
                            "\nСписок статистики игроков:");
                        while (reader.Read())
                        {
                            PrintStatFromReader(reader);
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

                        using var command = new SqlCommand(
                            "SELECT PlayerID, MatchID, " +
                            "PointsScored, IsStarter " +
                            "FROM PlayerMatchStats " +
                            "WHERE PlayerID = @PlayerId " +
                            "AND MatchID = @MatchId",
                            connection);
                        command.Parameters.AddWithValue(
                            "@PlayerId", playerId);
                        command.Parameters.AddWithValue(
                            "@MatchId", matchId);
                        using var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            PrintStatFromReader(reader);
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }

                        break;
                    }
                case "3":
                    {
                        var stat = ReadPlayerMatchStatFromConsole();
                        using var command = new SqlCommand(
                            "INSERT INTO PlayerMatchStats " +
                            "(PlayerID, MatchID, PointsScored, " +
                            "IsStarter) " +
                            "VALUES (@PlayerId, @MatchId, " +
                            "@PointsScored, @IsStarter)",
                            connection);
                        command.Parameters.AddWithValue(
                            "@PlayerId", stat.PlayerId);
                        command.Parameters.AddWithValue(
                            "@MatchId", stat.MatchId);
                        command.Parameters.AddWithValue(
                            "@PointsScored", stat.PointsScored);
                        command.Parameters.AddWithValue(
                            "@IsStarter", stat.IsStarter);
                        command.ExecuteNonQuery();
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

                        Console.Write("PointsScored: ");
                        int points = int.Parse(
                            Console.ReadLine()?.Trim() ?? "0");
                        Console.Write("IsStarter (1/0): ");
                        bool isStarter =
                            Console.ReadLine()?.Trim() == "1";
                        using var command = new SqlCommand(
                            "UPDATE PlayerMatchStats SET " +
                            "PointsScored = @PointsScored, " +
                            "IsStarter = @IsStarter " +
                            "WHERE PlayerID = @PlayerId " +
                            "AND MatchID = @MatchId",
                            connection);
                        command.Parameters.AddWithValue(
                            "@PlayerId", playerId);
                        command.Parameters.AddWithValue(
                            "@MatchId", matchId);
                        command.Parameters.AddWithValue(
                            "@PointsScored", points);
                        command.Parameters.AddWithValue(
                            "@IsStarter", isStarter);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Статистика обновлена."
                            : "Запись не найдена.");
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

                        using var command = new SqlCommand(
                            "DELETE FROM PlayerMatchStats " +
                            "WHERE PlayerID = @PlayerId " +
                            "AND MatchID = @MatchId",
                            connection);
                        command.Parameters.AddWithValue(
                            "@PlayerId", playerId);
                        command.Parameters.AddWithValue(
                            "@MatchId", matchId);
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0
                            ? "Статистика удалена."
                            : "Запись не найдена.");
                        break;
                    }
                default:
                    Console.WriteLine("Неверная операция.");
                    break;
            }
        }
    }
}
