namespace SortVisualizer.Client.Classes.SortElements
{
    public interface ISvgElement
    {
        Point GetStartPosition();
        Point GetEndPosition();
        Point GetFixedPosition();
        float GetValue();
        void Move(Point newPosition);
    }

}
