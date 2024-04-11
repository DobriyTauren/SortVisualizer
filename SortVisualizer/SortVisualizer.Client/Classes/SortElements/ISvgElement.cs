namespace SortVisualizer.Client.Classes.SortElements
{
    public interface ISvgElement
    {
        Point GetStartPosition();
        Point GetEndPosition();
        int GetValue();
        void Move(Point newPosition);
    }

}
