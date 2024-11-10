using BusinessObject;

namespace Repositories.FOOD;

public class FishFoodCalculatorRepository : IFishFoodCalculatorRepository
{
    public void AddNewFoodCalculator(FoodCalculator foodCalculator)
    {
        using var _dbContext = new KoiCareContext();
        _dbContext.Add(foodCalculator);
        _dbContext.SaveChanges();
    }

    public void DeleteFoodCalculator(int id)
    {
        using var _dbContext = new KoiCareContext();
        var existingFoodCalculator = _dbContext.FoodCalculators.Find(id);
        if (existingFoodCalculator != null)
        {
            _dbContext.Remove(existingFoodCalculator);
            _dbContext.SaveChanges();
        }
    }

    public List<FoodCalculator> GetAll()
    {
        using var _dbContext = new KoiCareContext();
        return _dbContext.FoodCalculators.ToList();
    }

    public void UpdateFoodCalculator(FoodCalculator foodCalculator)
    {
        using var _dbContext = new KoiCareContext();
        var existingFoodCalculator = _dbContext.FoodCalculators.Find(foodCalculator.CalcId);

        if (existingFoodCalculator != null)
        {
            existingFoodCalculator.FishId = foodCalculator.FishId;
            existingFoodCalculator.FoodTypeId = foodCalculator.FoodTypeId;
            existingFoodCalculator.RecommendedAmount = foodCalculator.RecommendedAmount;
            existingFoodCalculator.CalculationDate = foodCalculator.CalculationDate;

            _dbContext.SaveChanges();
        }
    }

    public FoodCalculator GetFoodCalculator(int id)
    {
        using var _dbContext = new KoiCareContext();
        return _dbContext.FoodCalculators.Find(id) ?? throw new InvalidOperationException();
    }


}