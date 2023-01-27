using dotnet_api_test.Exceptions.ExceptionResponses;
using dotnet_api_test.Persistence.Repositories.Interfaces;

namespace dotnet_api_test.Persistence.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly AppDbContext _context;

        public DishRepository(AppDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Dish> GetAllDishes()
        {
            return _context.Dishes.ToList();
        }

        public dynamic? GetAverageDishPrice()
        {
            var dishes = GetAllDishes();
            int counting = dishes.Count();
            var result = counting > 0 ? Math.Round(dishes.Average(d => d.Cost), 2) : 0.0;
            return result;
        }

        public Dish GetDishById(int Id)
        {
            var dish = _context.Dishes.Find(Id);
            if (dish == null)
                throw new NotFoundRequestExceptionResponse($"The dish with id: {Id} was not found");
            return dish;
        }

        public void DeleteDishById(int Id)
        {
            var dish = GetDishById(Id);
            if (dish == null)
                throw new NotFoundRequestExceptionResponse($"The dish with id: {Id} was not found");
            _context.Dishes.Remove(dish);
            SaveChanges();
        }

        public Dish CreateDish(Dish dish)
        {
            if (_context.Dishes.Any(d => d.Name == dish.Name))
                throw new BadRequestExceptionResponse("The name needs to be unique");
            _context.Dishes.Add(dish);
            SaveChanges();
            return dish;
        }

        public Dish UpdateDish(Dish dish)
        {
             if (_context.Dishes.Any(d => d.Name == dish.Name))
                throw new BadRequestExceptionResponse("The name needs to be unique");
            var update = _context.Dishes.Update(dish);
            SaveChanges();
            return update.Entity;
        }
    }
}