using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class GrowthRecord
{
    public int RecordId { get; set; }

    public int FishId { get; set; }

    public DateOnly MeasurementDate { get; set; }

    public decimal Length { get; set; }

    public decimal Weight { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Fish Fish { get; set; } = null!;
}
