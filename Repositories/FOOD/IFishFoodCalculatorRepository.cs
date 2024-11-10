using BusinessObject;

namespace Repositories.FOOD;

public interface IFishFoodCalculatorRepository
{
    void AddNewFoodCalculator(FoodCalculator foodCalculator);
    void DeleteFoodCalculator(int id);
    List<FoodCalculator> GetAll();
    void UpdateFoodCalculator(FoodCalculator foodCalculator);
    FoodCalculator GetFoodCalculator(int id);
}