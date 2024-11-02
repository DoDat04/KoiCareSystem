using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class WaterParameter
{
    public int ParameterId { get; set; }

    public int PondId { get; set; }

    public DateTime MeasurementDate { get; set; }

    public decimal PH { get; set; }

    public decimal Ammonia { get; set; }

    public decimal Nitrite { get; set; }

    public decimal Nitrate { get; set; }

    public decimal Temperature { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Pond Pond { get; set; } = null!;
}
