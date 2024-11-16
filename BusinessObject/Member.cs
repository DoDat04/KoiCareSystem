using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Member
{
    public int MemberId { get; set; }

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";


    public string? CreateBy { get; set; }

    public DateTimeOffset? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Fish> Fish { get; set; } = new List<Fish>();

    public virtual ICollection<Pond> Ponds { get; set; } = new List<Pond>();
}
