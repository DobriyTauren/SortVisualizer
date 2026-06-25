namespace SortVisualizer.Client.Classes.SortElements
{
    public interface ISvgElement
    {
        int Id { get; }

        Point GetStartPosition();
        Point GetEndPosition();
        Point GetFixedPosition();
        float GetValue();
        void Move(Point newPosition);

        /// <summary>Capture the current visual state (positions + color).</summary>
        ElementState Capture();

        /// <summary>Restore a previously captured visual state.</summary>
        void Apply(ElementState state);
    }

}
