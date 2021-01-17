using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreBoard;

namespace ScoreBoardTests
{
    [TestClass()]
    class GameTests
    {
        [TestMethod()]
        public void ShouldCreateGame()
        {
            var game = new Game("homeTeam", "awayTeam");

            Assert.AreEqual(0,game.TotalScoreGoals);
            Assert.AreEqual("homeTeam 0 - awayTeam 0",game.Result);
        }
    }
}
