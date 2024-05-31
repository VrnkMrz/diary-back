using System;
using System.Collections.Generic;

namespace diary_back.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int BirthdayYear { get; set; }

    public int BirthdayMonth { get; set; }

    public int BirthdayDay { get; set; }

    public int? Rank { get; set; }

    public int? CompanyId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<Diaryentry> Diaryentries { get; set; } = new List<Diaryentry>();

    public virtual Rank? RankNavigation { get; set; }
}
