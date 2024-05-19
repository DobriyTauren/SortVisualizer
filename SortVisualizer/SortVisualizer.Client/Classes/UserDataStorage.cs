using Blazored.LocalStorage;

namespace SortVisualizer.Client.classes
{
    public class UserDataStorage
    {
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
        
        public List<HistoryModel>? SmallHistory { get; private set; } = new List<HistoryModel>();
        public List<HistoryModel> FullHistory { get; private set; } = new List<HistoryModel>();
        public List<HistoryModel> FilteredHistory { get; private set; } = new List<HistoryModel>();
        public List<HistoryModel> SelectedHistory { get; private set; } = new List<HistoryModel>();

        public event EventHandler AlgorithmsChanged;
        public event EventHandler HistoryChanged;
        public event EventHandler FilteredHistoryChanged;

        public void AddToSelected (HistoryModel history)
        {
            SelectedHistory.Add(history);

            OnHistoryChanged();
        }

        public void ClearSelected()
        {
            SelectedHistory.Clear();

            OnHistoryChanged();
        }

        public void DeleteSelectedRecord (HistoryModel history)
        {
            SelectedHistory.Remove(history);

            OnHistoryChanged();
        }

        public async Task DeleteAllRecords(ILocalStorageService localStorage, IndexedDB indexedDB)
        {
            FullHistory.Clear();
            FilteredHistory.Clear();
            SelectedHistory.Clear();

            await indexedDB.DeleteAllObjects(localStorage);

            OnFilteredHistoryChanged();
        }

        public async Task DeleteSelectedRecords (ILocalStorageService localStorage, IndexedDB indexedDB)
        {
            // Create a hash set for SelectedHistory for faster lookups
            var selectedHashSet = new HashSet<HistoryModel>(SelectedHistory);

            FullHistory.RemoveAll(item => selectedHashSet.Contains(item));
            FilteredHistory.RemoveAll(item => selectedHashSet.Contains(item));

            await indexedDB.DeleteObjects(SelectedHistory, localStorage);

            SelectedHistory.Clear();
            OnFilteredHistoryChanged();
        }

        public void SetAlgorithms (List<AlgorithmModel> algorithms)
        {
            Algorithms = algorithms;

            OnAlgorithmsChanged();
        }

        public void SetSmallHistory (List<HistoryModel> history)
        {
            SmallHistory = history.TakeLast(10).Reverse().ToList();

            OnHistoryChanged();
        }
        
        public void SetFullHistory (List<HistoryModel> history)
        {
            FullHistory = history;

            OnHistoryChanged();
        }

        public void SetFilteredHistory(List<HistoryModel> history)
        {
            FilteredHistory = history;

            OnFilteredHistoryChanged();
        }

        public async Task AddHistory (HistoryModel history, ILocalStorageService localStorage, IndexedDB indexedDB)
        {
            try
            {
                await indexedDB.SaveObject(history, localStorage);
                
                FullHistory.Add(history);

                if (SmallHistory.Count == 10)
                {
                    SmallHistory.Insert(0, history);

                    SmallHistory.Remove(SmallHistory.Last());
                }
                else
                {
                    SmallHistory.Insert(0, history);
                }
            }
            catch
            {
                Console.WriteLine($"[indexedDB] - error when adding record at " + DateTime.Now.ToLongTimeString());
            }

            OnHistoryChanged();
        }

        private void OnAlgorithmsChanged () 
        {
            AlgorithmsChanged?.Invoke(null, EventArgs.Empty);
        }
        
        private void OnHistoryChanged () 
        {
            HistoryChanged?.Invoke(null, EventArgs.Empty);
        }

        private void OnFilteredHistoryChanged()
        {
            FilteredHistoryChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
