using TicTacToe.Application;
using TicTacToe.Domain;

namespace TicTacToe.Test;

public class GameTests
{
    [Fact]
    public void Player1ShouldWinWithRightDirection()
    {
        // Arrange
        var game = new Game("TestGame", 3);
        var player1 = new Player("P1");
        var player2 = new Player("P2");

        // Act
        Assert.True(game.SetColPlayer(0, 2, player1).result); // P1 selects (0, 0)
        Assert.True(game.SetColPlayer(1, 0, player2).result); // P2 selects (1, 0)
        Assert.True(game.SetColPlayer(1, 1, player1).result); // P1 selects (0, 1)
        Assert.True(game.SetColPlayer(0, 0, player2).result); // P2 selects (1, 1)
        Assert.True(game.SetColPlayer(2, 0, player1).result); // P1 selects (0, 2)
        
        // Assert
        var result = game.HaveWinner();
        //game.WriteOnScreen();
        Assert.True(result.result, "Player 1 should win with a right direction.");
        Assert.Equal(player1, result.player);
    }

    [Fact]
    public void Player2ShouldWinWithDownDirection()
    {
        // Arrange
        var game = new Game("TestGame", 3);
        var player1 = new Player("P1");
        var player2 = new Player("P2");

        // Act
        Assert.True(game.SetColPlayer(0, 0, player1).result); // P1 selects (0, 0)
        Assert.True(game.SetColPlayer(0, 1, player2).result); // P2 selects (0, 1)
        Assert.True(game.SetColPlayer(1, 0, player1).result); // P1 selects (1, 0)
        Assert.True(game.SetColPlayer(1, 1, player2).result); // P2 selects (1, 1)
        Assert.True(game.SetColPlayer(2, 2, player1).result); // P1 selects (2, 0)
        Assert.True(game.SetColPlayer(2, 1, player2).result); // P2 selects (2, 1)

        // Assert
        var result = game.HaveWinner();
        Assert.True(result.result, "Player 2 should win with a down direction.");
        Assert.Equal(player2, result.player);
    }

    [Fact]
    public void RandomMoveTestWithNoWinner()
    {
        // Arrange
        var game = new Game("TestGame", 3);
        var player1 = new Player("P1");
        var player2 = new Player("P2");

        // Act
        Assert.True(game.SetColPlayer(0, 0, player1).result); // P1 selects (0, 0)
        Assert.True(game.SetColPlayer(1, 0, player2).result); // P2 selects (1, 0)
        Assert.True(game.SetColPlayer(0, 1, player1).result); // P1 selects (0, 1)
        Assert.True(game.SetColPlayer(1, 1, player2).result); // P2 selects (1, 1)
        Assert.True(game.SetColPlayer(2, 2, player1).result); // P1 selects (0, 2)
        Assert.True(game.SetColPlayer(2, 0, player2).result); // P2 selects (2, 0)

        // Assert
        var result = game.HaveWinner();
        Assert.False(result.result, "There should be no winner.");
        Assert.Null(result.player);
    }

    [Fact]
    public void RandomMoveWithDiagonalWinner()
    {
        // Arrange
        var game = new Game("TestGame", 3);
        var player1 = new Player("P1");
        var player2 = new Player("P2");

        // Act
        Assert.True(game.SetColPlayer(0, 0, player1).result); // P1 selects (0, 0)
        Assert.True(game.SetColPlayer(0, 1, player2).result); // P2 selects (0, 1)
        Assert.True(game.SetColPlayer(1, 1, player1).result); // P1 selects (1, 1)
        Assert.True(game.SetColPlayer(2, 0, player2).result); // P2 selects (2, 0)
        Assert.True(game.SetColPlayer(2, 2, player1).result); // P1 selects (2, 2)

        // Assert
        var result = game.HaveWinner();
        Assert.True(result.result, "Player 1 should win with a diagonal direction.");
        Assert.Equal(player1, result.player);
    }

    [Fact]
    public void TestInvalidMoves()
    {
        // Arrange
        var game = new Game("TestGame", 3);
        var player1 = new Player("P1");
        var player2 = new Player("P2");

        // Act
        Assert.True(game.SetColPlayer(0, 0, player1).result); // P1 selects (0, 0)
        var invalidMove = game.SetColPlayer(0, 0, player2); // P2 tries to select (0, 0) again

        // Assert
        Assert.False(invalidMove.result, "Player 2 should not be able to choose a cell already selected.");
        Assert.Equal("It Chosen by P1 Before", invalidMove.message);
    }

    [Fact]
    public void FullBoardNoWinner()
    {
        // Arrange
        var game = new Game("TestGame", 3);
        var player1 = new Player("P1");
        var player2 = new Player("P2");

        // Act
        Assert.True(game.SetColPlayer(0, 0, player1).result);
        Assert.True(game.SetColPlayer(0, 1, player2).result);
        Assert.True(game.SetColPlayer(0, 2, player1).result);
        Assert.True(game.SetColPlayer(1, 0, player2).result);
        Assert.True(game.SetColPlayer(1, 1, player1).result);
        Assert.True(game.SetColPlayer(2, 2, player2).result);
        Assert.True(game.SetColPlayer(2, 0, player1).result);
        Assert.True(game.SetColPlayer(2, 1, player2).result);
        Assert.True(game.SetColPlayer(1, 2, player1).result);
        //game.WriteOnScreen();
        // Assert
        var result = game.HaveWinner();
        Assert.False(result.result, "There should be no winner on a full board.");
        Assert.Null(result.player);
    }
}