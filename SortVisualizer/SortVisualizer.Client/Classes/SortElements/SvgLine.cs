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
        // Вычисление разности между старой и новой позициями
        var deltaX = newPosition.X - StartPoint.X;
        var deltaY = newPosition.Y - StartPoint.Y;

        // Перемещение начальной и конечной точек линии на ту же разницу
        StartPoint = new Point(StartPoint.X + deltaX, StartPoint.Y + deltaY);
        EndPoint = new Point(EndPoint.X + deltaX, EndPoint.Y + deltaY);
    }

    public int GetValue() // kek yyyyyyyy
    {
        return Value;
    }

    public Point GetFixedPosition()
    {
        return FixedStartPoint;
    }
}