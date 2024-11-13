using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Fish
{
    public int FishId { get; set; }

    public int PondId { get; set; }

    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Length { get; set; }

    public string? ImagePath { get; set; }  // New property for the image path

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

    public Fish(int pondId, int memberId, string? imagePath, string name, decimal length, decimal weight, DateTime birthDate, string gender, string breed, bool isActive, DateTime? createDate)
    {
        PondId = pondId;
        MemberId = memberId;
        Name = name;
        Length = length;
        Weight = weight;
        BirthDate = birthDate;
        Gender = gender;
        Breed = breed;
        ImagePath = imagePath;  // Initialize ImagePath
        IsActive = isActive;
        CreateDate = createDate ?? DateTime.Now;
    }
}
