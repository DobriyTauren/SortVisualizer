namespace SortVisualizer.Client.Classes.SortElements
{
    public interface ISvgElement
    {
        Point GetPosition();
        Point GetFixedPosition();
        float GetValue();
        void Move(Point newPosition);
    }

}
