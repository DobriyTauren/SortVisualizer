public class SortTimer
{
    private TimeSpan _elapsedTime;
    private bool _isTimerRunning;
    private Timer _timer;

    public TimeSpan ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }

    public void Start()
    {
        if (!_isTimerRunning)
        {
            _isTimerRunning = true;
            ElapsedTime = TimeSpan.Zero;
            _timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }
    }

    public void Stop()
    {
        if (_isTimerRunning)
        {
            _isTimerRunning = false;
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

    private void TimerCallback(object state)
    {
        ElapsedTime = ElapsedTime.Add(TimeSpan.FromSeconds(1));
    }
}