using Microsoft.Data.SqlClient;
using VLStatsCodeSQL.Models;

namespace VLStatsCodeSQL
{
    public partial class Program
    {
        private static void PrintPlayer(Player player)
        {
            Console.WriteLine(
                $"  ID: {player.PlayerId}, " +
                $"{player.FirstName} {player.LastName}, " +
                $"Номер: {player.JerseyNumber}, " +
                $"Высота: {player.SpikeHeight?.ToString() ?? "N/A"}, " +
                $"Правша: {(player.IsRightHanded ? "Да" : "Нет")}, " +
                $"Команда: {player.TeamId?.ToString() ?? "N/A"}");
        }

        private static void PrintMatch(Match match)
        {
            Console.WriteLine(
                $"  ID: {match.MatchId}, " +
                $"Дата: {match.MatchDate:yyyy-MM-dd HH:mm}, " +
                $"Турнир: {match.TournamentId?.ToString() ?? "N/A"}, " +
                $"Дома: {match.HomeTeamId?.ToString() ?? "N/A"}, " +
                $"Гости: {match.AwayTeamId?.ToString() ?? "N/A"}");
        }

        private static void PrintPlayerMatchStat(
            PlayerMatchStat stat)
        {
            Console.WriteLine(
                $"  PlayerId: {stat.PlayerId}, " +
                $"MatchId: {stat.MatchId}, " +
                $"Очки: {stat.PointsScored}, " +
                $"Стартовый: {(stat.IsStarter ? "Да" : "Нет")}");
        }

        private static void PrintPlayerFromReader(
            SqlDataReader reader)
        {
            int playerId = reader.GetInt32(0);
            string firstName = reader.GetString(1);
            string lastName = reader.GetString(2);
            int jerseyNumber = reader.GetInt32(3);
            string spikeHeight = reader.IsDBNull(4)
                ? "N/A"
                : reader.GetDecimal(4).ToString();
            bool isRightHanded = reader.GetBoolean(5);
            string teamId = reader.IsDBNull(6)
                ? "N/A"
                : reader.GetInt32(6).ToString();

            Console.WriteLine(
                $"  ID: {playerId}, " +
                $"{firstName} {lastName}, " +
                $"Номер: {jerseyNumber}, " +
                $"Высота: {spikeHeight}, " +
                $"Правша: {(isRightHanded ? "Да" : "Нет")}, " +
                $"Команда: {teamId}");
        }

        private static void PrintMatchFromReader(
            SqlDataReader reader)
        {
            var matchId = reader.GetGuid(0);
            var matchDate = reader.GetDateTime(1);
            string tournamentId = reader.IsDBNull(2)
                ? "N/A"
                : reader.GetInt32(2).ToString();
            string homeTeamId = reader.IsDBNull(3)
                ? "N/A"
                : reader.GetInt32(3).ToString();
            string awayTeamId = reader.IsDBNull(4)
                ? "N/A"
                : reader.GetInt32(4).ToString();

            Console.WriteLine(
                $"  ID: {matchId}, " +
                $"Дата: {matchDate:yyyy-MM-dd HH:mm}, " +
                $"Турнир: {tournamentId}, " +
                $"Дома: {homeTeamId}, " +
                $"Гости: {awayTeamId}");
        }

        private static void PrintStatFromReader(
            SqlDataReader reader)
        {
            int playerId = reader.GetInt32(0);
            var matchId = reader.GetGuid(1);
            int points = reader.GetInt32(2);
            bool isStarter = reader.GetBoolean(3);

            Console.WriteLine(
                $"  PlayerId: {playerId}, " +
                $"MatchId: {matchId}, " +
                $"Очки: {points}, " +
                $"Стартовый: {(isStarter ? "Да" : "Нет")}");
        }

        private static Player ReadPlayerFromConsole()
        {
            Console.Write("Имя (FirstName): ");
            string firstName =
                Console.ReadLine()?.Trim() ?? "";
            Console.Write("Фамилия (LastName): ");
            string lastName =
                Console.ReadLine()?.Trim() ?? "";
            Console.Write("Номер (JerseyNumber): ");
            int jerseyNumber = int.Parse(
                Console.ReadLine()?.Trim() ?? "0");
            Console.Write(
                "Высота прыжка (SpikeHeight, " +
                "пусто если нет): ");
            string spikeInput =
                Console.ReadLine()?.Trim() ?? "";
            decimal? spikeHeight = string.IsNullOrEmpty(spikeInput)
                ? null
                : decimal.Parse(spikeInput);
            Console.Write("Правша? (1/0): ");
            bool isRightHanded =
                Console.ReadLine()?.Trim() == "1";
            Console.Write(
                "TeamId (пусто если нет): ");
            string teamInput =
                Console.ReadLine()?.Trim() ?? "";
            int? teamId = string.IsNullOrEmpty(teamInput)
                ? null
                : int.Parse(teamInput);

            return new Player
            {
                FirstName = firstName,
                LastName = lastName,
                JerseyNumber = jerseyNumber,
                SpikeHeight = spikeHeight,
                IsRightHanded = isRightHanded,
                TeamId = teamId
            };
        }

        private static void UpdatePlayerFromConsole(
            Player player)
        {
            Console.Write(
                $"Имя (текущее: {player.FirstName}): ");
            player.FirstName =
                Console.ReadLine()?.Trim() ?? player.FirstName;
            Console.Write(
                $"Фамилия (текущее: {player.LastName}): ");
            player.LastName =
                Console.ReadLine()?.Trim() ?? player.LastName;
            Console.Write(
                $"Номер (текущий: {player.JerseyNumber}): ");
            player.JerseyNumber = int.Parse(
                Console.ReadLine()?.Trim() ?? "0");
            Console.Write(
                $"Высота прыжка " +
                $"(текущая: {player.SpikeHeight?.ToString() ?? "N/A"}): ");
            string spikeInput =
                Console.ReadLine()?.Trim() ?? "";
            player.SpikeHeight = string.IsNullOrEmpty(spikeInput)
                ? null
                : decimal.Parse(spikeInput);
            Console.Write(
                $"Правша (текущее: " +
                $"{(player.IsRightHanded ? "1" : "0")}): ");
            player.IsRightHanded =
                Console.ReadLine()?.Trim() == "1";
            Console.Write(
                $"TeamId " +
                $"(текущий: {player.TeamId?.ToString() ?? "N/A"}): ");
            string teamInput =
                Console.ReadLine()?.Trim() ?? "";
            player.TeamId = string.IsNullOrEmpty(teamInput)
                ? null
                : int.Parse(teamInput);
        }

        private static Match ReadMatchFromConsole()
        {
            Console.Write(
                "Дата матча (yyyy-MM-dd HH:mm): ");
            var matchDate = DateTime.Parse(
                Console.ReadLine()?.Trim() ?? "");
            Console.Write(
                "TournamentId (пусто если нет): ");
            string tournamentInput =
                Console.ReadLine()?.Trim() ?? "";
            int? tournamentId =
                string.IsNullOrEmpty(tournamentInput)
                ? null
                : int.Parse(tournamentInput);
            Console.Write(
                "HomeTeamId (пусто если нет): ");
            string homeInput =
                Console.ReadLine()?.Trim() ?? "";
            int? homeTeamId = string.IsNullOrEmpty(homeInput)
                ? null
                : int.Parse(homeInput);
            Console.Write(
                "AwayTeamId (пусто если нет): ");
            string awayInput =
                Console.ReadLine()?.Trim() ?? "";
            int? awayTeamId = string.IsNullOrEmpty(awayInput)
                ? null
                : int.Parse(awayInput);

            return new Match
            {
                MatchDate = matchDate,
                TournamentId = tournamentId,
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId
            };
        }

        private static void UpdateMatchFromConsole(Match match)
        {
            Console.Write(
                $"Дата (текущая: " +
                $"{match.MatchDate:yyyy-MM-dd HH:mm}): ");
            match.MatchDate = DateTime.Parse(
                Console.ReadLine()?.Trim() ?? "");
            Console.Write(
                $"TournamentId " +
                $"(текущий: {match.TournamentId?.ToString() ?? "N/A"}): ");
            string tournamentInput =
                Console.ReadLine()?.Trim() ?? "";
            match.TournamentId =
                string.IsNullOrEmpty(tournamentInput)
                ? null
                : int.Parse(tournamentInput);
            Console.Write(
                $"HomeTeamId " +
                $"(текущий: {match.HomeTeamId?.ToString() ?? "N/A"}): ");
            string homeInput =
                Console.ReadLine()?.Trim() ?? "";
            match.HomeTeamId = string.IsNullOrEmpty(homeInput)
                ? null
                : int.Parse(homeInput);
            Console.Write(
                $"AwayTeamId " +
                $"(текущий: {match.AwayTeamId?.ToString() ?? "N/A"}): ");
            string awayInput =
                Console.ReadLine()?.Trim() ?? "";
            match.AwayTeamId = string.IsNullOrEmpty(awayInput)
                ? null
                : int.Parse(awayInput);
        }

        private static PlayerMatchStat
            ReadPlayerMatchStatFromConsole()
        {
            Console.Write("PlayerId: ");
            int playerId = int.Parse(
                Console.ReadLine()?.Trim() ?? "0");
            Console.Write("MatchId (GUID): ");
            var matchId = Guid.Parse(
                Console.ReadLine()?.Trim() ?? "");
            Console.Write("PointsScored: ");
            int points = int.Parse(
                Console.ReadLine()?.Trim() ?? "0");
            Console.Write("IsStarter (1/0): ");
            bool isStarter =
                Console.ReadLine()?.Trim() == "1";

            return new PlayerMatchStat
            {
                PlayerId = playerId,
                MatchId = matchId,
                PointsScored = points,
                IsStarter = isStarter
            };
        }

        private static void UpdatePlayerMatchStatFromConsole(
            PlayerMatchStat stat)
        {
            Console.Write(
                $"PointsScored " +
                $"(текущее: {stat.PointsScored}): ");
            stat.PointsScored = int.Parse(
                Console.ReadLine()?.Trim() ?? "0");
            Console.Write(
                $"IsStarter " +
                $"(текущее: {(stat.IsStarter ? "1" : "0")}): ");
            stat.IsStarter =
                Console.ReadLine()?.Trim() == "1";
        }
    }
}
