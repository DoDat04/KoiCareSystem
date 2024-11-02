using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class FoodType
{
    public int FoodTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<FoodCalculator> FoodCalculators { get; set; } = new List<FoodCalculator>();
}
