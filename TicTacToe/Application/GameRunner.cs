using TicTacToe.Domain;

namespace TicTacToe.Application;

public class GameRunner
{
    private Game game { get; set; }
    private List<Player> Players = new();
    private Player? nowIsTurn { get; set; }

    public GameRunner(string gameName, int gameSize)
    {
        game = new Game(gameName, gameSize);
        Players.Add(new Player("P1"));
        Players.Add(new Player("P2"));
    }

    public void Start()
    {
        do
        {
            Act();
            game.WriteOnScreen();
        } while (!game.HaveWinner().result);
    }

    public Game Game => game;

    private string Act()
    {
        nowIsTurn ??= Players.First();
        Console.WriteLine($" GO NOW {nowIsTurn.Name} : ");
        var xPosition = (short)GetFromUser<int>("Enter X: ")!;
        var yPosition = (short)GetFromUser<int>("Enter Y: ")!;
        var setColResult = game.SetColPlayer(xPosition, yPosition, player: nowIsTurn);

        if (setColResult.result)
        {
            nowIsTurn = nowIsTurn == Players.First() ? Players.Last() : Players.First();
            return setColResult.message;
        }
    
        Console.WriteLine(setColResult.message);
        Console.WriteLine("Try Again");
        return Act();
    }

    private T? GetFromUser<T>(string message)
    {
        Console.WriteLine(message);
        try
        {
            return (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Field to get data, try again {exception.Message}");
            return GetFromUser<T>(message);
        }
    }
}