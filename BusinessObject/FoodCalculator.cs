using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class FoodCalculator
{
    public int CalcId { get; set; }

    public int? FishId { get; set; }

    public int FoodTypeId { get; set; }

    public decimal RecommendedAmount { get; set; }

    public DateTime CalculationDate { get; set; }

    public string? Notes { get; set; }

    public virtual Fish? Fish { get; set; }

    public virtual FoodType FoodType { get; set; } = null!;
}
