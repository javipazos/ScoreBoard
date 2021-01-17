using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ScoreBoardTests")] 
namespace ScoreBoard
{
    internal class Game
    {
        private int HomeScore { get; set; }
        private string HomeName { get; set; }
        private int AwayScore { get; set; }
        private string AwayName { get; set; }
        public int TotalScoreGoals => HomeScore + AwayScore;
        public string Result => $"{HomeName} {HomeScore} - {AwayName} {AwayScore}";
        internal DateTime LastUpdateTime{ get; set; }

        public Game(string homeTeam, string awayTeam)
        {
            HomeName = homeTeam;
            AwayName = awayTeam;
        }

        public ActionResult UpdateResult(in int homeScore, in int awayScore)
        {
            var actionResult = new ActionResult();
            if (homeScore < 0 || awayScore < 0)
            {
                actionResult.ErrorMessage = "Scores cannot be lower than 0";
                actionResult.Success = false;
                return actionResult;
            }
            HomeScore = homeScore;
            AwayScore = awayScore;
            LastUpdateTime = DateTime.Now;
            actionResult.Success = true;
            return actionResult;
        }
    }
}