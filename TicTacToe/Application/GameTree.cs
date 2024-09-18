using TicTacToe.Domain;

namespace TicTacToe.Application;

public class GameTree
{
    private List<Col> Cols { get; set; }
    private int Size { get; set; }

    public GameTree(List<Col> cols, int size)
    {
        Cols = cols;
        Size = size;
    }
    
    public Node BuildTree(Player player)
    {
        Node? root = null;
        
        foreach (var col in Cols.Where(c => c.SelectedBy == player))
        {
            if (root == null)
            {
                root = new Node(col);
            }
            else
            {
                AddChildNodes(root, col);
            }
        }

        return root!;
    }
    
    private void AddChildNodes(Node root, Col col)
    {
        if (root.Right == null && root.Col.XPosition == col.XPosition && root.Col.YPosition + 1 == col.YPosition)
        {
            root.Right = new Node(col);
        }
        else if (root.Down == null && root.Col.XPosition + 1 == col.XPosition && root.Col.YPosition == col.YPosition)
        {
            root.Down = new Node(col);
        }
        else if (root.Diagonal == null && root.Col.XPosition + 1 == col.XPosition && root.Col.YPosition + 1 == col.YPosition)
        {
            root.Diagonal = new Node(col);
        }
        else if (root.LeftDiagonal == null && root.Col.XPosition + 1 == col.XPosition && root.Col.YPosition - 1 == col.YPosition)
        {
            root.LeftDiagonal = new Node(col); // Add reverse diagonal connection
        }
        else
        {
            if (root.Right != null)
                AddChildNodes(root.Right, col);
            if (root.Down != null)
                AddChildNodes(root.Down, col);
            if (root.Diagonal != null)
                AddChildNodes(root.Diagonal, col);
            if (root.LeftDiagonal != null)
                AddChildNodes(root.LeftDiagonal, col); // Recursively handle reverse diagonal
        }
    }
    
    public bool CheckWinner(Node? node, int count = 1, Way? way = null)
    {
        if (node == null || node.Col.SelectedBy == null)
            return false;

        // Check if the current node belongs to the player
        var player = node.Col.SelectedBy;

        // If the count matches the board size, the player has won
        if (count == Size)
            return true;

        // Traverse in the specified direction
        if (way == Way.Right && node.Right != null && node.Right.Col.SelectedBy == player)
        {
            return CheckWinner(node.Right, count + 1, Way.Right);
        }
        else if (way == Way.Down && node.Down != null && node.Down.Col.SelectedBy == player)
        {
            return CheckWinner(node.Down, count + 1, Way.Down);
        }
        else if (way == Way.RightDown && node.Diagonal != null && node.Diagonal.Col.SelectedBy == player)
        {
            return CheckWinner(node.Diagonal, count + 1, Way.RightDown);
        }
        else if (way == Way.LeftDown && node.LeftDiagonal != null && node.LeftDiagonal.Col.SelectedBy == player)
        {
            return CheckWinner(node.LeftDiagonal, count + 1, Way.LeftDown); // New condition for reverse diagonal
        }

        // If this is the root node, check all four directions
        if (way == null)
        {
            return (node.Right != null && node.Right.Col.SelectedBy == player && CheckWinner(node.Right, count + 1, Way.Right)) ||
                   (node.Down != null && node.Down.Col.SelectedBy == player && CheckWinner(node.Down, count + 1, Way.Down)) ||
                   (node.Diagonal != null && node.Diagonal.Col.SelectedBy == player && CheckWinner(node.Diagonal, count + 1, Way.RightDown)) ||
                   (node.LeftDiagonal != null && node.LeftDiagonal.Col.SelectedBy == player && CheckWinner(node.LeftDiagonal, count + 1, Way.LeftDown)); // Check reverse diagonal
        }

        return false;
    }
}