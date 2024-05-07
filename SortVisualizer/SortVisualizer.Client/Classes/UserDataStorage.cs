using Microsoft.JSInterop;

namespace SortVisualizer.Client.classes
{
    public class UserDataStorage
    {
        private SaveAPI _saveAPI;
        public AlgorithmModel CurrentAlgorithm { get; set; }
        public List<AlgorithmModel>? Algorithms { get; private set; }
        public List<HistoryModel>? UserHistories { get; private set; } = new List<HistoryModel>();

        public event EventHandler DataChanged;

        public void SetAlgorithms (List<AlgorithmModel> algorithms)
        {
            Algorithms = algorithms;
        }

        public void SetHistory (List<HistoryModel> history)
        {
            UserHistories = history;

            OnDataChanged();
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
            _saveAPI.AddHistory(history);

            OnDataChanged();
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

        private void OnDataChanged () 
        {
            DataChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
