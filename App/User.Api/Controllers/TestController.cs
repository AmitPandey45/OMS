using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("info")]
        public IActionResult LogInformationMessage()
        {
            _logger.LogInformation("This is an information message.");
            return Ok("Information message logged.");
        }

        [HttpGet("warning")]
        public IActionResult LogWarningMessage()
        {
            _logger.LogWarning("This is a warning message.");
            return Ok("Warning message logged.");
        }

        [HttpGet("error")]
        public IActionResult LogErrorMessage()
        {
            _logger.LogError("This is an error message.");
            return Ok("Error message logged.");
        }

        [HttpGet("exception")]
        public IActionResult LogException()
        {
            try
            {
                throw new InvalidOperationException("Simulated exception in TestController.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in TestController.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("data")]
        public IActionResult LogData([FromBody] TestData data)
        {
            _logger.LogInformation($"Received data: Name={data.Name}, Value={data.Value}");
            return Ok("Data logged.");
        }

        [HttpGet("performance")]
        public IActionResult LogPerformance()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            // Simulate some work
            System.Threading.Thread.Sleep(100);
            startTime.Stop();

            _logger.LogInformation($"Performance: Operation took {startTime.ElapsedMilliseconds} ms.");
            return Ok($"Operation took {startTime.ElapsedMilliseconds} ms.");
        }

        [HttpGet("context")]
        public IActionResult LogContextualData()
        {
            _logger.LogInformation("Logging request context.");
            //The context information is added by the context logger.
            return Ok("Context logged.");
        }

        public class TestData
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}
