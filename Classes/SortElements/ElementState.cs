namespace SortVisualizer.Client.Classes.SortElements
{
    /// <summary>
    /// Immutable visual snapshot of one element (positions + color).
    /// For lines P1 = start, P2 = end. For circles P1 = center (P2 unused).
    /// </summary>
    public readonly record struct ElementState(double P1X, double P1Y, double P2X, double P2Y, string Color);

    /// <summary>A change of a single element (identified by stable Id) within one frame.</summary>
    public readonly record struct Delta(int Id, ElementState State);

    /// <summary>One animation frame: the set of elements that changed since the previous frame.</summary>
    public sealed class Frame
    {
        public List<Delta> Deltas { get; }

        public Frame(List<Delta> deltas)
        {
            Deltas = deltas;
        }
    }
}
