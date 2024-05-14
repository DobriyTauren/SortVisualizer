using SortVisualizer.Client.Classes.SortElements;

public class SvgLine : SvgShape, ISvgElement
{
    public Point Position { get; set; }

    public Point FixedPosition { get; set; }
    
    public Point GetPosition()
    {
        return Position;
    }

    public void Move(Point newPosition)
    {
        Position = new Point(newPosition.X, Position.Y);
    }

    public float GetValue() // kek yyyyyyyy
    {
        return Value;
    }

    public Point GetFixedPosition()
    {
        return FixedPosition;
    }
}