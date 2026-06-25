using SortVisualizer.Client.Classes.SortElements;

public class SvgCircle : SvgShape, ISvgElement
{
    public Point Center { get; set; }
    public Point FixedCenter { get; set; }

    public int Radius { get; set; }

    public Point GetStartPosition()
    {
        return Center;
    }

    public Point GetEndPosition()
    {
        return Center;
    }

    public void Move(Point newPosition)
    {
        Center = newPosition;
    }

    public float GetValue()
    {
        return Value;
    }

    public Point GetFixedPosition()
    {
        return FixedCenter;
    }

    public ElementState Capture()
    {
        return new ElementState(Center.X, Center.Y, Center.X, Center.Y, Color);
    }

    public void Apply(ElementState state)
    {
        Center = new Point(state.P1X, state.P1Y);
        Color = state.Color;
    }

    public SvgCircle Clone()
    {
        return new SvgCircle
        {
            Id = Id,
            Value = Value,
            Color = Color,
            Radius = Radius,
            Center = new Point(Center.X, Center.Y),
            FixedCenter = new Point(FixedCenter.X, FixedCenter.Y),
        };
    }
}