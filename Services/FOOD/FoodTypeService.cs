using BusinessObject;
using Repositories.FOOD;

namespace Services.FOOD;

public class FoodTypeService(IFoodTypeRepository foodTypeRepository) : IFoodTypeService
{
    public IEnumerable<FoodType> GetAllActive()
    {
        return foodTypeRepository.GetAllActive();
    }
}