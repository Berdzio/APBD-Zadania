using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class Wytwornie
{
    public int IdWytworni { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Albumy> Albumies { get; set; } = new List<Albumy>();
}
