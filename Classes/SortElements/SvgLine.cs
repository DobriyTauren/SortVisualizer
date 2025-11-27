using SortVisualizer.Client.Classes.SortElements;

public class SvgLine : SvgShape, ISvgElement
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }

    public Point FixedStartPoint { get; set; }
    
    public Point GetStartPosition()
    {
        return StartPoint;
    }

    public Point GetEndPosition()
    {
        return EndPoint;
    }

    public void Move(Point newPosition)
    {
        var deltaX = newPosition.X - StartPoint.X;
        var deltaY = newPosition.Y - StartPoint.Y;

        StartPoint = newPosition;
        EndPoint = new Point(EndPoint.X + deltaX, EndPoint.Y + deltaY);
    }

    public float GetValue() // kek yyyyyyyy
    {
        return Value;
    }

    public Point GetFixedPosition()
    {
        return FixedStartPoint;
    }
}