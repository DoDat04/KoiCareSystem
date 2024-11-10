using BusinessObject;
using Repositories.FOOD;

namespace Services;

public class FoodTypeService(IFoodTypeRepository foodTypeRepository) : IFoodTypeService
{
    public IEnumerable<FoodType> GetAllActive()
    {
        return foodTypeRepository.GetAllActive();
    }
}