using SortVisualizer.Client.Classes.SortElements;

public class SvgCircle : SvgShape, ISvgElement
{
    public Point Center { get; set; }
    public int Radius { get; set; }

    public Point GetStartPosition()
    {
        // Начальная позиция центра круга
        return Center;
    }

    public Point GetEndPosition()
    {
        // Конечная позиция центра круга
        return Center;
    }

    public void Move(Point newPosition)
    {
        // Перемещение круга
        Center = newPosition;
    }

    public int GetValue()
    {
        return Value;
    }
}