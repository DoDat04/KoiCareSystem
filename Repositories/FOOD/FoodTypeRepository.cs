using BusinessObject;

namespace Repositories.FOOD;

public class FoodTypeRepository : IFoodTypeRepository
{
    public IEnumerable<FoodType> GetAllActive()
    {
        using var context = new KoiCareContext();
        return context.FoodTypes
            .Where(f => f.IsActive)
            .OrderBy(ft => ft.Name)
            .ToList();
    }
}