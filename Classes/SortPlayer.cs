using SortVisualizer.Client.Classes.SortElements;

namespace SortVisualizer.Client.Classes
{
    /// <summary>
    /// Plays back a recorded sort (a list of <see cref="Frame"/>s) over the live elements.
    /// Supports play / pause / single step / seek / live speed change. Rendering is the
    /// caller's job: pass a render callback that triggers a re-render of the SVG.
    /// </summary>
    public class SortPlayer
    {
        private readonly List<ISvgElement> _elements;
        private readonly Dictionary<int, ISvgElement> _byId;
        private readonly Dictionary<int, ElementState> _initial;
        private readonly List<Frame> _frames;

        // Bumped on every pause / restart so a running Play loop knows to stop.
        private int _token;

        public SortPlayer(
            List<ISvgElement> elements,
            Dictionary<int, ElementState> initial,
            List<Frame> frames,
            int delay)
        {
            _elements = elements;
            _byId = elements.ToDictionary(e => e.Id);
            _initial = initial;
            _frames = frames;
            Delay = delay < 1 ? 1 : delay;
            Reset();
        }

        /// <summary>Number of frames already applied (0..Total).</summary>
        public int Index { get; private set; }

        public int Total => _frames.Count;
        public bool IsPlaying { get; private set; }
        public bool IsFinished => Index >= Total;
        public bool IsAtStart => Index <= 0;

        public int Delay { get; set; }

        /// <summary>Accumulated playback time in milliseconds (sum of applied frame delays).</summary>
        public double ElapsedMs { get; private set; }

        public void Reset()
        {
            foreach (var e in _elements)
            {
                if (_initial.TryGetValue(e.Id, out var state))
                    e.Apply(state);
            }

            Index = 0;
            ElapsedMs = 0;
        }

        private void ApplyFrame(Frame frame)
        {
            foreach (var d in frame.Deltas)
            {
                if (_byId.TryGetValue(d.Id, out var e))
                    e.Apply(d.State);
            }
        }

        public bool ApplyNext()
        {
            if (Index >= Total)
                return false;

            ApplyFrame(_frames[Index]);
            Index++;
            ElapsedMs += Delay;
            return true;
        }

        /// <summary>Jump to an arbitrary frame count by replaying from the nearest point.</summary>
        public void SeekTo(int target)
        {
            target = Math.Clamp(target, 0, Total);

            if (target < Index)
                Reset();

            while (Index < target)
            {
                ApplyFrame(_frames[Index]);
                Index++;
            }

            ElapsedMs = Index * (double)Delay;
        }

        public void StepForward(Action render)
        {
            Pause();
            if (ApplyNext())
                render();
        }

        public void StepBack(Action render)
        {
            Pause();
            SeekTo(Index - 1);
            render();
        }

        public async Task Play(Func<Task> render)
        {
            if (IsFinished)
                Reset();

            IsPlaying = true;
            var token = ++_token;

            while (IsPlaying && token == _token && Index < Total)
            {
                ApplyNext();
                await render();
                await Task.Delay(Delay);
            }

            if (token == _token)
                IsPlaying = false;
        }

        public void Pause()
        {
            IsPlaying = false;
            _token++;
        }
    }
}
