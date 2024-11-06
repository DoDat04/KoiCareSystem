using BusinessObject;

namespace Services;

public interface IFishFoodCalculatorService
{
    void AddNewFoodCalculator(FoodCalculator foodCalculator);
    void DeleteFoodCalculator(int id);
    List<FoodCalculator> GetAll();
    void UpdateFoodCalculator(FoodCalculator foodCalculator);
    FoodCalculator GetFoodCalculator(int id);
}