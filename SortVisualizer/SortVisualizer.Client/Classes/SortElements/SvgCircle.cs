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
}