using Microsoft.JSInterop;

namespace SortVisualizer.Client.classes
{
    public class UserDataStorage
    {
        private SaveAPI _saveAPI;
        private int _itemsCount = 75;
        private int _delay = 1;

        public int Delay
        {
            get => _delay;
            set
            {
                switch (value)
                {
                    case < 0:
                        _delay = 0;
                        break;
                    case > 9999:
                        _delay = 9999;
                        break;
                    default:
                        _delay = value;
                        break;
                }
            }
        }

        public int ItemsCount
        {
            get => _itemsCount;
            set
            {
                switch (value)
                {
                    case < 1:
                        _itemsCount = 1;
                        break;
                    case > 355:
                        _itemsCount = 355;
                        break;
                    default:
                        _itemsCount = value;
                        break;
                }
            }
        }

        public AlgorithmModel CurrentAlgorithm { get; set; }
        public List<AlgorithmModel> Algorithms { get; private set; } = new List<AlgorithmModel>();
        public List<HistoryModel>? UserHistories { get; private set; } = new List<HistoryModel>();

        public event EventHandler AlgorithmsChanged;

        public event EventHandler HistoryChanged;

        public void SetAlgorithms (List<AlgorithmModel> algorithms)
        {
            Algorithms = algorithms;

            OnAlgorithmsChanged();
        }

        public void SetHistory (List<HistoryModel> history)
        {
            UserHistories = history;

            OnHistoryChanged();
        }

        public async Task AddHistory (HistoryModel history)
        {
            if (UserHistories.Count == 10)
            {
                UserHistories.Insert(0, history);

                UserHistories.Remove(UserHistories.Last());
            }
            else
            {
                UserHistories.Insert(0, history);
            }

            _saveAPI = new SaveAPI(this);
            _saveAPI.TryAddHistory(history);

            OnHistoryChanged();
        }

        public async Task<string> GetUserId (IJSRuntime jSRuntime)
        {
            var userId = await jSRuntime.InvokeAsync<string>("localStorage.getItem", "UserId");

            if (string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString();
                await jSRuntime.InvokeVoidAsync("localStorage.setItem", "UserId", userId);
            }

            return userId;
        }

        private void OnAlgorithmsChanged () 
        {
            AlgorithmsChanged?.Invoke(null, EventArgs.Empty);
        }
        
        private void OnHistoryChanged () 
        {
            HistoryChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
