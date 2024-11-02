﻿using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Fish
{
    public int FishId { get; set; }

    public int PondId { get; set; }

    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Length { get; set; }

    public decimal Weight { get; set; }

    public DateTime BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<FoodCalculator> FoodCalculators { get; set; } = new List<FoodCalculator>();

    public virtual ICollection<GrowthRecord> GrowthRecords { get; set; } = new List<GrowthRecord>();

    public virtual Member Member { get; set; } = null!;

    public virtual Pond Pond { get; set; } = null!;
}
