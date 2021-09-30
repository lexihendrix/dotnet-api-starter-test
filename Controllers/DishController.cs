using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_api_test.Controllers
{
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;

        public DishController(ILogger<DishController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}