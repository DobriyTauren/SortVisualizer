using Microsoft.AspNetCore.Components;
using SortVisualizer.Client.classes;
using System.Net.Http.Json;

public class SaveAPI
{
    private const string HTTP_PATH = "http://localhost:7248/";
    private const string HTTPS_PATH = "https://localhost:7248/";

    private readonly UserDataStorage _globalData;
    private readonly NavigationManager _navigationManager;
    private HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(HTTPS_PATH) };

    public SaveAPI(UserDataStorage globalData, NavigationManager navigationManager)
    {
        _globalData = globalData;
        _navigationManager = navigationManager;
    }

    public SaveAPI(UserDataStorage globalData)
    {
        _globalData = globalData;
    }

    public async Task<AlgorithmModel> TryGetAlgorithmsWithSelected ()
    {
        AlgorithmModel? selectedAlgorithm = new AlgorithmModel() { Name = "", Description = "", CodeName = "" };

        try // PEREDELAT
        {
            selectedAlgorithm = await GetAlgorithmsWithSelected(_httpClient);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            selectedAlgorithm = await GetAlgorithmsWithSelected(_httpClient);
        }

        return selectedAlgorithm;
    }

    private async Task<AlgorithmModel> GetAlgorithmsWithSelected(HttpClient httpClient)
    {
        if (_globalData.Algorithms.Count == 0)
        {
            _globalData.SetAlgorithms(await httpClient.GetFromJsonAsync<List<AlgorithmModel>>("api/algorithms"));
        }
        string relativePath = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

        var selectedAlgorithm = _globalData.Algorithms.FirstOrDefault(a => a.CodeName == relativePath);

        return selectedAlgorithm;
    }

    public async Task TryGetAlgorithms()
    {
        try // PEREDELAT
        {
            await GetAlgorithms(_httpClient);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            await GetAlgorithms(_httpClient);
        }
    }

    private async Task GetAlgorithms(HttpClient httpClient)
    {
        if (_globalData.Algorithms.Count == 0)
        {
            _globalData.SetAlgorithms(await httpClient.GetFromJsonAsync<List<AlgorithmModel>>("api/algorithms"));
        }
    }
}
