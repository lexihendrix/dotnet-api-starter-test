using AutoMapper;
using dotnet_api_test.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_api_test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IMapper _mapper;
        private readonly IDishRepository _dishRepository;

        public DishController(ILogger<DishController> logger, IMapper mapper, IDishRepository dishRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _dishRepository = dishRepository;
        }

        [HttpGet]
        [Route("")] 
        public ActionResult GetDishes()
        {
            return Ok();
        }
        
        [HttpGet]
        [Route("{id}")] 
        public ActionResult GetDishById(int id)
        {
            return Ok();
        }
        
        [HttpPost]
        [Route("")] 
        public ActionResult CreateDish()
        {
            return Ok();
        }
        
        [HttpPut]
        [Route("{id}")] 
        public ActionResult UpdateDishById(int id)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")] 
        public ActionResult DeleteDishById(int id)
        {
            return Ok();
        }
    }
}