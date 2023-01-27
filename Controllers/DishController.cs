using AutoMapper;
using dotnet_api_test.Exceptions.ExceptionResponses;
using dotnet_api_test.Models.Dtos;
using dotnet_api_test.Persistence.Repositories.Interfaces;
using dotnet_api_test.Validation;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<DishesAndAveragePriceDto> GetDishesAndAverageDishPrice()
        {
            try
            {
                var all = _dishRepository.GetAllDishes();
                var allDto = _mapper.Map<IEnumerable<ReadDishDto>>(all);
                var average = _dishRepository.GetAverageDishPrice();
                return Ok(new DishesAndAveragePriceDto { AveragePrice = average, Dishes = allDto });
            }
            catch (BadRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem: ", e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ReadDishDto> GetDishById(int id)
        {
            try
            {
                var dish = _dishRepository.GetDishById(id);
                return Ok(_mapper.Map<ReadDishDto>(dish));
            }
            catch (BadRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem fetching the dish: ", e);
                return BadRequest(e.Message);
            }
            catch (NotFoundRequestExceptionResponse e)
            {
                _logger.LogError("The dish was not found: ", e);
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<ReadDishDto> CreateDish([FromBody] CreateDishDto createDishDto)
        {
            try
            {
                ModelValidation.ValidateCreateDishDto(createDishDto);
                var dish = _mapper.Map<Dish>(createDishDto);
                _dishRepository.CreateDish(dish);
                return Ok(_mapper.Map<ReadDishDto>(dish));
            }
            catch (BadRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem creating the dish: ", e);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ReadDishDto> UpdateDishById(int id, UpdateDishDto updateDishDto)
        {
            try
            {
                var dishToUpdate = _dishRepository.GetDishById(id);
                ModelValidation.ValidateUpdateDishDto(updateDishDto, dishToUpdate.Cost);
                _mapper.Map(updateDishDto, dishToUpdate);
                _dishRepository.UpdateDish(dishToUpdate);
                return Ok(_mapper.Map<ReadDishDto>(dishToUpdate));
            }
            catch (BadRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem when updating dish: ", e);
                return BadRequest(e.Message);
            }
            catch (NotFoundRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem finding the dish: ", e);
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDishById(int id)
        {
            try
            {
                _dishRepository.DeleteDishById(id);
                _logger.LogInformation($"The dish deleted with id: ", id);
                return NoContent();
            }
            catch (BadRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem with deleting dish: ", e);
                return BadRequest(e.Message);
            }
            catch (NotFoundRequestExceptionResponse e)
            {
                _logger.LogError("There was a problem finding dish: ", e);
                return NotFound(e.Message);
            }
        }
    }
}