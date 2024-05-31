using System;
using System.Collections.Generic;

namespace diary_back.Models;

public partial class Rank
{
    public int Id { get; set; }

    public string RankName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
