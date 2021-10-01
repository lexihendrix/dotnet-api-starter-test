using System.Collections.Generic;

namespace dotnet_api_test.Persistence.Repositories.Interfaces
{
    public interface IDishRepository
    {
        bool SaveChanges();
        IEnumerable<Dish> GetAllDishes();
        Dish GetDishById(int Id);
        void DeleteDishById();
        Dish CreateDish(Dish dish);
        Dish UpdateDish(Dish dish);
    }
}