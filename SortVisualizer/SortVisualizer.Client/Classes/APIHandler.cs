using Microsoft.AspNetCore.Components;
using SortVisualizer.Client.classes;
using System.Net.Http.Json;

public class APIHandler
{
    private const string HTTP_PATH = "http://46.32.185.2:5000/";

    private HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(HTTP_PATH) };

    public async Task CheckForAlgorithms (UserDataStorage userData, NavigationManager navigationManager)
    {
        if (userData.Algorithms.Count > 0)
        {
            string relativePath = navigationManager.ToBaseRelativePath(navigationManager.Uri);

            var _selectedAlgorithm = userData.Algorithms.FirstOrDefault(a => a.CodeName == relativePath);

            if (_selectedAlgorithm != null)
            {
                userData.CurrentAlgorithm = _selectedAlgorithm;
            }
        }
        else
        {
            var _selectedAlgorithm = await TryGetAlgorithmsWithSelected(userData, navigationManager);

            userData.CurrentAlgorithm = _selectedAlgorithm;
        }
    }

    public async Task CheckForAlgorithms(UserDataStorage userData)
    {
        if (userData.Algorithms.Count > 0)
        {
            return;
        }
        else
        {
            await GetAlgorithms(_httpClient, userData);
        }
    }

    public async Task<AlgorithmModel> TryGetAlgorithmsWithSelected (UserDataStorage userData, NavigationManager navigationManager)
    {
        AlgorithmModel? selectedAlgorithm = new AlgorithmModel() { Name = "", Description = "", CodeName = "" };
        
        selectedAlgorithm = await GetAlgorithmsWithSelected(_httpClient, userData, navigationManager);

        return selectedAlgorithm;
    }

    private async Task<AlgorithmModel> GetAlgorithmsWithSelected(HttpClient httpClient, UserDataStorage userData, NavigationManager navigationManager)
    {
        if (userData.Algorithms.Count == 0)
        {
            userData.SetAlgorithms(await httpClient.GetFromJsonAsync<List<AlgorithmModel>>("api/algorithms"));
        }

        string relativePath = navigationManager.ToBaseRelativePath(navigationManager.Uri);

        var selectedAlgorithm = userData.Algorithms.FirstOrDefault(a => a.CodeName == relativePath);

        return selectedAlgorithm;
    }

    private async Task GetAlgorithms(HttpClient httpClient, UserDataStorage userData)
    {
        if (userData.Algorithms.Count == 0)
        {
            userData.SetAlgorithms(await httpClient.GetFromJsonAsync<List<AlgorithmModel>>("api/algorithms"));
        }
    }
}
