using System;
using System.Collections.Generic;
using ScoreBoard;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScoreBoardTests
{
    [TestClass()]
    public class ScoreBoardTests
    {
        [TestMethod()]
        public void ShouldStartGameWhenThereIsNoOtherForSameTeams()
        {
            var scoreBoards = new ScoreBoard.ScoreBoard();
            Assert.IsTrue(scoreBoards.StartGame("homeTeam", "awayTeam").Success);
            Assert.AreEqual("homeTeam 0 - awayTeam 0", scoreBoards.GetScoreBoard().Single());
        }

        [TestMethod()]
        public void ShouldFailStartGameWhenThereIsOtherForSameTeams()
        {
            var scoreBoards = new ScoreBoard.ScoreBoard();
            _ = scoreBoards.StartGame("homeTeam", "awayTeam");
            Assert.IsFalse(scoreBoards.StartGame("homeTeam", "awayTeam").Success);
        }

        [TestMethod()]
        public void ShouldUpdateGameScore()
        {
            var homeTeam = "homeTeam";
            var awayTeam = "awayTeam";
            var homeScore = 2;
            var awayScore = 1;
            var scoreBoards = new ScoreBoard.ScoreBoard();
            _ = scoreBoards.StartGame(homeTeam, awayTeam);

            Assert.IsTrue(scoreBoards.UpdateGameScore(homeTeam, awayTeam, homeScore, awayScore).Success);
            Assert.AreEqual("homeTeam 2 - awayTeam 1", scoreBoards.GetScoreBoard().Single());
        }

        [TestMethod()]
        public void FinishGameTest()
        {
            var homeTeam = "homeTeam";
            var awayTeam = "awayTeam";

            var scoreBoards = new ScoreBoard.ScoreBoard();
            _ = scoreBoards.StartGame(homeTeam, awayTeam);

            Assert.IsTrue(scoreBoards.FinishGame(homeTeam, awayTeam).Success);
            Assert.AreEqual("The score board is empty", scoreBoards.GetScoreBoard().Single());
        }

        [TestMethod()]
        public void ShouldReturnScoreBoardOrderByTotalScoreThenByMostRecent()
        {
            var scoreBoards = new ScoreBoard.ScoreBoard();

            scoreBoards.StartGame("Mexico", "Canada");
            scoreBoards.UpdateGameScore("Mexico", "Canada", 0, 5);
            scoreBoards.StartGame("Spain", "Brazil");
            scoreBoards.UpdateGameScore("Spain", "Brazil", 10, 2);
            scoreBoards.StartGame("Germany", "France");
            scoreBoards.UpdateGameScore("Germany", "France", 2, 2);
            scoreBoards.StartGame("Uruguay", "Italy");
            scoreBoards.UpdateGameScore("Uruguay", "Italy", 6, 6);
            scoreBoards.StartGame("Argentina", "Australia");
            scoreBoards.UpdateGameScore("Argentina", "Australia", 3, 1);

            var scoreBoard = scoreBoards.GetScoreBoard();

            var expected = new List<string>()
            {
                "Uruguay 6 - Italy 6",
                "Spain 10 - Brazil 2",
                "Mexico 0 - Canada 5",
                "Argentina 3 - Australia 1",
                "Germany 2 - France 2"
            };
            Assert.AreEqual(string.Join(',',expected),string.Join(',',scoreBoard));
        }
    }
}