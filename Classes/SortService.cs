using SortVisualizer.Client.Classes.SortElements;

/// <summary>
/// Records a sort run as a list of animation frames instead of animating in real time.
/// Algorithms keep calling <see cref="WaitColor"/> at every "look at this element" point;
/// each call captures a frame by diffing every element's visual state against the previous
/// frame, so swaps AND direct moves (merge, insert, shell) are all picked up automatically.
/// No Task.Delay / re-render happens here — playback is driven separately by SortPlayer.
/// </summary>
public class SortService
{
    public string BaseColor { get; private set; } = "#46B5D9";
    public string ActionColor { get; private set; } = "#E86AA6";
    public string SortedColor { get; private set; } = "#4CC79C";

    /// <summary>
    /// When true, WaitColor does nothing — used for a pure-algorithm timing pass
    /// (Stopwatch) where the per-frame diff would otherwise pollute the measurement.
    /// </summary>
    public bool Silent { get; set; }

    private List<ISvgElement> _elements = new();
    private Dictionary<int, ElementState> _prev = new();

    // Recording runs on the single UI thread; yield to the browser every so often
    // so the page stays responsive (and can show a "preparing" state) instead of freezing.
    private int _sinceYield;
    private const int YieldEvery = 256;

    /// <summary>Frames captured during the last recording, in order.</summary>
    public List<Frame> Frames { get; private set; } = new();

    /// <summary>Visual state of every element at the moment recording started (keyed by Id).</summary>
    public Dictionary<int, ElementState> InitialStates { get; private set; } = new();

    /// <summary>
    /// Begin capturing. Call right before running the algorithm, once the elements are
    /// in their starting visual arrangement.
    /// </summary>
    public void StartRecording(IEnumerable<ISvgElement> elements)
    {
        _elements = elements.ToList();
        Frames = new List<Frame>();
        _sinceYield = 0;
        _prev = new Dictionary<int, ElementState>(_elements.Count);
        InitialStates = new Dictionary<int, ElementState>(_elements.Count);

        foreach (var e in _elements)
        {
            var state = e.Capture();
            _prev[e.Id] = state;
            InitialStates[e.Id] = state;
        }
    }

    /// <summary>
    /// Capture a trailing frame after the algorithm finishes — the last swap/move usually
    /// happens after the final WaitColor, so without this it would be missing from playback.
    /// </summary>
    public void EndRecording()
    {
        Tick();
    }

    private void Tick()
    {
        var deltas = new List<Delta>();

        foreach (var e in _elements)
        {
            var state = e.Capture();
            if (!state.Equals(_prev[e.Id]))
            {
                deltas.Add(new Delta(e.Id, state));
                _prev[e.Id] = state;
            }
        }

        Frames.Add(new Frame(deltas));
    }

    /// <summary>
    /// Marks a frame at the current step. For line elements the examined element flashes
    /// in the action color for exactly this frame (captured by the diff, then reverted).
    /// The <paramref name="delay"/> is ignored — timing belongs to playback.
    /// </summary>
    public async Task WaitColor<T>(int delay, T elem) where T : ISvgElement
    {
        if (Silent)
            return;

        if (elem is SvgLine line)
        {
            line.Color = ActionColor;
            Tick();
            line.Color = BaseColor;
        }
        else
        {
            Tick();
        }

        // periodically hand control back to the browser so recording doesn't freeze the UI
        if (++_sinceYield >= YieldEvery)
        {
            _sinceYield = 0;
            await Task.Yield();
        }
    }
}
