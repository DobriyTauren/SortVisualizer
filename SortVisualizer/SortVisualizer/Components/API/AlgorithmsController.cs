using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AlgorithmsController : ControllerBase
{
    private readonly AlgorithmsContext _context;

    private readonly ILogger<AlgorithmsController> _logger;

    public AlgorithmsController(IServiceProvider serviceProvider, ILogger<AlgorithmsController> logger)
    {
        _context = serviceProvider.GetRequiredService<AlgorithmsContext>();

        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlgorithmModel>>> GetAlgorithms()
    {
        _logger.LogInformation("[api/algorithms] - visited at " + DateTime.Now.ToLongTimeString());

        return await _context.Algorithms.ToListAsync();
    }
}
