using Microsoft.AspNetCore.Components;
using SortVisualizer.Client.classes;
using System.Net.Http.Json;

public class APIHandler
{
    private const string HTTP_PATH = "http://localhost:5000/";
    private const string HTTPS_PATH = "https://localhost:5001";

    private HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(HTTPS_PATH) };

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
            await TryGetAlgorithms(userData);
        }
    }

    public async Task<AlgorithmModel> TryGetAlgorithmsWithSelected (UserDataStorage userData, NavigationManager navigationManager)
    {
        AlgorithmModel? selectedAlgorithm = new AlgorithmModel() { Name = "", Description = "", CodeName = "" };

        try // PEREDELAT
        {
            selectedAlgorithm = await GetAlgorithmsWithSelected(_httpClient, userData, navigationManager);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            selectedAlgorithm = await GetAlgorithmsWithSelected(_httpClient, userData, navigationManager);
        }

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

    public async Task TryGetAlgorithms(UserDataStorage userData)
    {
        try // PEREDELAT
        {
            await GetAlgorithms(_httpClient, userData);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            await GetAlgorithms(_httpClient, userData);
        }
    }

    private async Task GetAlgorithms(HttpClient httpClient, UserDataStorage userData)
    {
        if (userData.Algorithms.Count == 0)
        {
            userData.SetAlgorithms(await httpClient.GetFromJsonAsync<List<AlgorithmModel>>("api/algorithms"));
        }
    }
}
