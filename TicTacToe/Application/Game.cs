using TicTacToe.Domain;

namespace TicTacToe.Application;

public enum Way
{
    Right,
    Down,
    RightDown,
    LeftDown
}

public class Game
{
    private int Size { get; set; } = 3;
    private string Name { get; set; } = "Sample";

    private List<Col> Cols { get; set; } = new List<Col>();

    public Game(string name, int size)
    {
        Size = size;
        Name = name;

        for (short x = 0; x < Size; x++)
        {
            for (short y = 0; y < Size; y++)
            {
                Cols?.Add(new Col(x, y));
            }
        }
    }

    public void WriteOnScreen()
    {
        Console.Clear();
        Console.WriteLine("Current Board:");
        for (short x = 0; x < Size; x++)
        {
            string row = "";
            for (short y = 0; y < Size; y++)
            {
                var col = GetCol(x, y);
                if (col.SelectedBy == null)
                {
                    row += $"{x}{y} ";
                }
                else
                {
                    row += $"{col.SelectedBy.Name} ";
                }
            }
            Console.WriteLine(row);
        }
    }

    public Col? GetCol(short xpos, short ypos, Player? player = null)
    {
        return Cols.FirstOrDefault(x =>
            x.XPosition == xpos && x.YPosition == ypos && (player == null || x.SelectedBy == player));
    }

    public (bool result, string message) SetColPlayer(short xpos, short ypos, Player player)
    {
        var col = GetCol(xpos, ypos);
        if (col == null)
            return (false, "Col Not Exists");
        if (col.SelectedBy != null)
            return (false, $"It Chosen by {col.SelectedBy.Name!} Before");
        col.SelectedBy = player;
        return (true, "Done");
    }

    public (bool result , Player? player) HaveWinner()
    {
        var players = Cols.GroupBy(x => x.SelectedBy).Select(x => x.Key).Where(x => x != null).ToList();

        // Build a tree for each player and check for a winner
        foreach (var player in players)
        {
            var gameTree = new GameTree(Cols, Size);
            var playerTree = gameTree.BuildTree(player!);

            // Traverse the tree to check if the player has won
            if (gameTree.CheckWinner(playerTree))
            {
                Console.WriteLine($"Player {player!.Name} wins!");
                return (true,player);
            }
        }

        Console.WriteLine("No winner yet.");
        return (false,null);
    }
}



