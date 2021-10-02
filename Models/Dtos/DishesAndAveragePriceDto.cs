using System.Collections.Generic;

namespace dotnet_api_test.Models.Dtos
{
    public class DishesAndAveragePriceDto
    {
        public IEnumerable<ReadDishDto> Dishes { get; set; }
        public double AveragePrice { get; set; }

        public DishesAndAveragePriceDto()
        {
            Dishes = new List<ReadDishDto>();
            AveragePrice = 0;
        }
    }
}