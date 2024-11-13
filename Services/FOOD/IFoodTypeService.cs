using BusinessObject;

namespace Services.FOOD;

public interface IFoodTypeService
{
    IEnumerable<FoodType> GetAllActive();
}