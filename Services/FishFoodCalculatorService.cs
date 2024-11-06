using BusinessObject;
using Repositories;

namespace Services;

public class FishFoodCalculatorService(IFishFoodCalculatorRepository fishFoodCalculatorRepository)
    : IFishFoodCalculatorService
{
    public void AddNewFoodCalculator(FoodCalculator foodCalculator)
    {
        fishFoodCalculatorRepository.AddNewFoodCalculator(foodCalculator);
    }
    
    public void DeleteFoodCalculator(int id)
    {
        fishFoodCalculatorRepository.DeleteFoodCalculator(id);
    }
    
    public List<FoodCalculator> GetAll()
    {
        return fishFoodCalculatorRepository.GetAll();
    }
    
    public void UpdateFoodCalculator(FoodCalculator foodCalculator)
    {
        fishFoodCalculatorRepository.UpdateFoodCalculator(foodCalculator);
    }
    
    public FoodCalculator GetFoodCalculator(int id)
    {
        return fishFoodCalculatorRepository.GetFoodCalculator(id);
    }
}