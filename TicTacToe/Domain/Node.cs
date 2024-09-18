namespace TicTacToe.Domain;

public class Node
{
    public Col Col { get; set; }
    public Node? Right { get; set; }
    public Node? Down { get; set; }
    public Node? Diagonal { get; set; }
    public Node? LeftDiagonal { get; set; } // New node for reverse diagonal

    public Node(Col col)
    {
        Col = col;
    }
}