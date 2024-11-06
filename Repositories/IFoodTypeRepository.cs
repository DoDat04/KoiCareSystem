using BusinessObject;

namespace Repositories;

public interface IFoodTypeRepository
{
    IEnumerable<FoodType> GetAllActive();
}