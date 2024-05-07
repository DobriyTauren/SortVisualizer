using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[ApiController]
public class HistoriesController : ControllerBase
{
    private readonly AlgorithmsContext _context;

    private readonly ILogger<AlgorithmsController> _logger;

    public HistoriesController(IServiceProvider serviceProvider, ILogger<AlgorithmsController> logger)
    {
        _context = serviceProvider.GetRequiredService<AlgorithmsContext>();

        _logger = logger;
    }

    [HttpGet]
    [Route("api/histories")]
    public async Task<ActionResult<IEnumerable<HistoryModel>>> GetHistories()
    {
        return await _context.Histories.ToListAsync();
    }

    [HttpGet]
    [Route("api/histories/{userId}")]
    public async Task<ActionResult<IEnumerable<HistoryModel>>> GetHistoriesByUserId(string userId)
    {
        var histories = await _context.Histories.Where(h => h.UserId == userId).ToListAsync();

        return histories;
    }

    [HttpGet]
    [Route("api/histories/{userId}&{itemsCount}")]
    public async Task<ActionResult<IEnumerable<HistoryModel>>> GetHistoriesByUserId(string userId, int itemsCount)
    {
        var histories = await _context.Histories.Where(h => h.UserId == userId).ToListAsync();

        histories = histories.TakeLast(itemsCount).Reverse().ToList();

        return histories;
    }

    [HttpPost]
    [Route("api/histories/add")]
    public IActionResult AddRecordToDatabase([FromBody] JsonElement requestData)
    {
        if (requestData.TryGetProperty("token", out var tokenElement) && tokenElement.ValueKind == JsonValueKind.String)
        {
            string token = tokenElement.GetString();

            var tokenService = new TokenService();

            if (!tokenService.ValidateToken(token))
            {
                return Unauthorized();
            }
        }
        else
        {
            return BadRequest("Token is missing or invalid.");
        }

        // Проверка наличия ключа "record" в JSON
        if (requestData.TryGetProperty("record", out var recordElement) && recordElement.ValueKind == JsonValueKind.Object)
        {
            // Десериализация объекта HistoryModel из JSON
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр свойств
            };
            HistoryModel record = JsonSerializer.Deserialize<HistoryModel>(recordElement.GetRawText(), options);

            _context.Histories.Add(record);
            _context.SaveChanges();

            return Ok("Record added successfully.");
        }
        else
        {
            return BadRequest("Record data is missing or invalid.");
        }
    }
}