using System;
using System.Collections.Generic;

namespace diary_back.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public int? Commander { get; set; }

    public virtual User? CommanderNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
