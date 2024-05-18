using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[ApiController]
public class HistoriesController : ControllerBase
{
    private readonly AlgorithmsContext _context;

    private readonly ILogger<HistoriesController> _logger;

    public HistoriesController(IServiceProvider serviceProvider, ILogger<HistoriesController> logger)
    {
        _context = serviceProvider.GetRequiredService<AlgorithmsContext>();

        _logger = logger;
    }

    [HttpGet]
    [Route("api/histories")]
    public async Task<ActionResult<IEnumerable<HistoryModel>>> GetHistories()
    {
        _logger.LogInformation("[api/histories] - visited at " + DateTime.Now.ToLongTimeString());

        return await _context.Histories.ToListAsync();
    }

    [HttpGet]
    [Route("api/histories/{userId}")]
    public async Task<ActionResult<IEnumerable<HistoryModel>>> GetHistoriesByUserId(string userId)
    {
        _logger.LogInformation($"[api/histories/{userId}] - visited at " + DateTime.Now.ToLongTimeString());

        var histories = await _context.Histories.Where(h => h.UserId == userId).ToListAsync();

        return histories;
    }

    [HttpGet]
    [Route("api/histories/{userId}&{itemsCount}")]
    public async Task<ActionResult<IEnumerable<HistoryModel>>> GetHistoriesByUserId(string userId, int itemsCount)
    {
        _logger.LogInformation($"[api/histories/{userId}&{itemsCount}] - visited at " + DateTime.Now.ToLongTimeString());

        var histories = await _context.Histories.Where(h => h.UserId == userId).ToListAsync();

        histories = histories.TakeLast(itemsCount).Reverse().ToList();

        return histories;
    }

    [HttpPost]
    [Route("api/histories/add")]
    public IActionResult AddRecordToDatabase([FromBody] JsonElement requestData)
    {
        _logger.LogInformation("[api/histories/add] - visited at " + DateTime.Now.ToLongTimeString());

        if (requestData.TryGetProperty("token", out var tokenElement) && tokenElement.ValueKind == JsonValueKind.String)
        {
            string token = tokenElement.GetString();

            var tokenService = new TokenService();

            if (!tokenService.ValidateToken(token))
            {
                _logger.LogWarning("[api/histories/add] - token is invalid");

                return Unauthorized();
            }
        }
        else
        {
            _logger.LogWarning("[api/histories/add] - token is missing or invalid");

            return BadRequest("Token is missing or invalid.");
        }

        // Проверка наличия ключа "record" в JSON
        if (requestData.TryGetProperty("record", out var recordElement) && recordElement.ValueKind == JsonValueKind.Object)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр свойств
            };
            HistoryModel record = JsonSerializer.Deserialize<HistoryModel>(recordElement.GetRawText(), options);

            _context.Histories.Add(record);
            _context.SaveChanges();

            _logger.LogInformation("[api/histories/add] - record added successfully");

            return Ok("Record added successfully.");
        }
        else
        {
            _logger.LogWarning("[api/histories/add] - record data is missing or invalid");

            return BadRequest("Record data is missing or invalid.");
        }
    }
}