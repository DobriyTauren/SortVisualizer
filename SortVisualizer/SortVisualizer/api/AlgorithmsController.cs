using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        return await _context.Algorithms.ToListAsync();
    }
}
