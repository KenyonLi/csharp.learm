using Lifetime.Demo.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lifetime.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly ICounterService _counterService;
        private readonly ILogger<CounterController> _logger;
        public CounterController(ILogger<CounterController> logger,ICounterService counterService)
        {
            _logger = logger;
            _counterService = counterService;
        }
        [HttpGet("increment")]
        public ActionResult<int> Increment()
        {
            _counterService.Increment();
            _logger.LogInformation($"Operation ID: {_counterService.GetOperationId()} - Incrementing");
            return _counterService.GetCount();
        }

        [HttpGet("count")]
        public ActionResult<int> GetCount()
        {
            _logger.LogInformation($"Operation ID: {_counterService.GetOperationId()} - Getting Count");
            return _counterService.GetCount();
        }


        [HttpGet("operationId")]
        public ActionResult<Guid> GetOperationId()
        {
            _logger.LogInformation($"Operation ID: {_counterService.GetOperationId()} - Getting Operation ID");
            return _counterService.GetOperationId();
        }
    }
}
