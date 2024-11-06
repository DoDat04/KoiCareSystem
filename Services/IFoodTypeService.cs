using BusinessObject;

namespace Services;

public interface IFoodTypeService
{
    IEnumerable<FoodType> GetAllActive();
}