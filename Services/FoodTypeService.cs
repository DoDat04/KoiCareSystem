using BusinessObject;
using Repositories;

namespace Services;

public class FoodTypeService(IFoodTypeRepository foodTypeRepository) : IFoodTypeService
{
    public IEnumerable<FoodType> GetAllActive()
    {
        return foodTypeRepository.GetAllActive();
    }
}