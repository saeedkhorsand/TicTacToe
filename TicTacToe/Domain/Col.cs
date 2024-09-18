namespace TicTacToe.Domain;

public class Col
{
    public Col(short xPosition, short yPosition)
    {
        XPosition = xPosition;
        YPosition = yPosition;
    }
    public Player? SelectedBy { get; set; }
    public short XPosition { get; set; }
    public short YPosition { get; set; }
    public short Order => Convert.ToInt16($"{XPosition}{YPosition}");
}