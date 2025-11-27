using Blazored.LocalStorage;

public class IndexedDB
{
    public ObjectManager ObjectManager {  get; set; }

    public async Task SaveObject(HistoryModel historyModel, ILocalStorageService localStorage)
    {
        ObjectManager.History.Add(historyModel);

        await localStorage.SetItemAsync("objectManager", ObjectManager); 
    }

    public async Task LoadObjects(ILocalStorageService localStorage)
    {
        ObjectManager = await localStorage.GetItemAsync<ObjectManager>("objectManager");
        if (ObjectManager == null)
        {
            ObjectManager = new ObjectManager();
        }
    }

    public async Task DeleteObjects(List<HistoryModel> historyModels, ILocalStorageService localStorage)
    {
        foreach (var model in historyModels)
        {
            var itemToRemove = ObjectManager.History.FirstOrDefault(h => h.Id == model.Id);
            if (itemToRemove != null)
            {
                ObjectManager.History.Remove(itemToRemove);
            }
        }

        await localStorage.SetItemAsync("objectManager", ObjectManager); 
    }

    public async Task DeleteAllObjects(ILocalStorageService localStorage)
    {
        ObjectManager.History.Clear(); 

        await localStorage.SetItemAsync("objectManager", ObjectManager); 
    }
}