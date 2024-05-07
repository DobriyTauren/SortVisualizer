using Microsoft.AspNetCore.Components;
using SortVisualizer.Client.classes;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class SaveAPI
{
    private const string HTTP_PATH = "http://localhost:7248/";
    private const string HTTPS_PATH = "https://localhost:7248/";

    public string Token {  get; private set; }  

    private readonly UserDataStorage _globalData;
    private readonly NavigationManager _navigationManager;
    private TokenService _tokenService = new TokenService();
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

    public async Task AddHistory(HistoryModel historyModel)
    {
        if (Token == null || _tokenService.IsTokenExpired(Token))
        {
            Token = _tokenService.GenerateToken();
        }

        try // PEREDELAT
        {
            var data = new { token = Token, record = historyModel };

            var jsonData = JsonSerializer.Serialize(data);

            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/histories/add", content);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/histories/add", new { Token, historyModel });
            response.EnsureSuccessStatusCode();
        }
    }

    public async Task GetHistory (string userId)
    {
        try // PEREDELAT
        {
            await GetHistoryByUserId(_httpClient, userId);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            await GetHistoryByUserId(_httpClient, userId);
        }
    }

    private async Task GetHistoryByUserId(HttpClient httpClient, string userId)
    {
        if (_globalData.UserHistories.Count == 0)
        {            
            var history = await httpClient.GetFromJsonAsync<List<HistoryModel>>($"api/histories/{userId}&10"); // get 10 last records

            _globalData.SetHistory(history);
        }
    }

    public async Task<AlgorithmModel> GetSelectedAlgorithm ()
    {
        AlgorithmModel? selectedAlgorithm = new AlgorithmModel() { Name = "", Description = "", CodeName = "" };

        try // PEREDELAT
        {
            selectedAlgorithm = await GetAlgorithms(_httpClient);
        }
        catch
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HTTP_PATH);

            selectedAlgorithm = await GetAlgorithms(_httpClient);
        }

        return selectedAlgorithm;
    }

    private async Task<AlgorithmModel> GetAlgorithms (HttpClient httpClient)
    {
        if (_globalData.Algorithms == null)
        {
            _globalData.SetAlgorithms(await httpClient.GetFromJsonAsync<List<AlgorithmModel>>("api/algorithms"));
        }
        string relativePath = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

        var selectedAlgorithm = _globalData.Algorithms.FirstOrDefault(a => a.CodeName == relativePath);

        return selectedAlgorithm;
    }
}
