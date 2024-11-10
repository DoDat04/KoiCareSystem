using BusinessObject;

namespace Repositories.FOOD;

public interface IFoodTypeRepository
{
    IEnumerable<FoodType> GetAllActive();
}