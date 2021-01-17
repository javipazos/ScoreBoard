using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreBoard
{
    public class ScoreBoard
    {
        private Dictionary<(string,string),Game> _scoreBoard = new Dictionary<(string, string), Game>();
        public ActionResult StartGame(string homeTeam, string awayTeam)
        {
            var actionResult = new ActionResult();
            if (_scoreBoard.ContainsKey((homeTeam, awayTeam)))
            {
                actionResult.ErrorMessage = $"There is already a game for {homeTeam} vs {awayTeam}";
                return actionResult;
            }
            _scoreBoard.Add((homeTeam,awayTeam),new Game(homeTeam,awayTeam));
            actionResult.Success = true;
            return actionResult;
        }

        public ActionResult UpdateGameScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var actionResult = new ActionResult();
            if (!_scoreBoard.TryGetValue((homeTeam, awayTeam),out var game))
            {
                actionResult.ErrorMessage = $"There is no a game for {homeTeam} vs {awayTeam}";
                actionResult.Success = false;
                return actionResult;
            }

            return  game.UpdateResult(homeScore,awayScore);
        }

        public ActionResult FinishGame(string homeTeam, string awayTeam)
        {
            var actionResult = new ActionResult();
            if (!_scoreBoard.ContainsKey((homeTeam, awayTeam)))
            {
                actionResult.ErrorMessage = $"There is no a game for {homeTeam} vs {awayTeam}";
                actionResult.Success = false;
                return actionResult;
            }
            _scoreBoard.Remove((homeTeam,awayTeam));
            actionResult.Success = true;
            return actionResult;
        }

        public IEnumerable<string> GetScoreBoard()
        {
            return _scoreBoard.Values.Any()
                ? _scoreBoard.Values.OrderByDescending(g => g.TotalScoreGoals).ThenByDescending(q=>q.LastUpdateTime).Select(g => $"{g.Result}")
                : new List<string>() {"The score board is empty"};
        }
    }


}
