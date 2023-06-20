using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class Firefighter
{
    public int IdFirefighter { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Action> IdActions { get; set; } = new List<Action>();
}
