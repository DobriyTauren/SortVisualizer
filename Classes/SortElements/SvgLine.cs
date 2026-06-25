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

    public ElementState Capture()
    {
        return new ElementState(StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, Color);
    }

    public void Apply(ElementState state)
    {
        StartPoint = new Point(state.P1X, state.P1Y);
        EndPoint = new Point(state.P2X, state.P2Y);
        Color = state.Color;
    }

    public SvgLine Clone()
    {
        return new SvgLine
        {
            Id = Id,
            Value = Value,
            Color = Color,
            StartPoint = new Point(StartPoint.X, StartPoint.Y),
            EndPoint = new Point(EndPoint.X, EndPoint.Y),
            FixedStartPoint = new Point(FixedStartPoint.X, FixedStartPoint.Y),
        };
    }
}