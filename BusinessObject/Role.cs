using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTimeOffset? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTimeOffset? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
