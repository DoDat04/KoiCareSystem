using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Pond
{
    public int PondId { get; set; }

    public string Name { get; set; } = null!;

    public int MemberId { get; set; }

    public decimal Length { get; set; }

    public decimal Width { get; set; }

    public decimal Depth { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<Fish> Fish { get; set; } = new List<Fish>();

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<WaterParameter> WaterParameters { get; set; } = new List<WaterParameter>();
}
