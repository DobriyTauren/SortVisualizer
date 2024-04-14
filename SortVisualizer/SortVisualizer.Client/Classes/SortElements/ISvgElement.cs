namespace SortVisualizer.Client.Classes.SortElements
{
    public interface ISvgElement
    {
        Point GetStartPosition();
        Point GetEndPosition();
        Point GetFixedPosition();
        int GetValue();
        void Move(Point newPosition);
    }

}
