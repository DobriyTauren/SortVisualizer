using Blazored.LocalStorage;

public class IndexedDB
{
    public ObjectManager ObjectManager {  get; set; }

    public async Task SaveObject(HistoryModel historyModel, ILocalStorageService localStorage)
    {
        ObjectManager.History.Add(historyModel);

        await localStorage.SetItemAsync("objectManager", ObjectManager); // Сохраняем объект и текущий максимальный ID
    }

    public async Task LoadObjects(ILocalStorageService localStorage)
    {
        ObjectManager = await localStorage.GetItemAsync<ObjectManager>("objectManager");
        if (ObjectManager == null)
        {
            ObjectManager = new ObjectManager();
        }
    }
}