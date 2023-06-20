using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class Utwory
{
    public int IdUtwor { get; set; }

    public string NazwaUtworu { get; set; } = null!;

    public float CzasTrwania { get; set; }

    public int IdStudies { get; set; }

    public virtual Albumy IdStudiesNavigation { get; set; } = null!;

    public virtual ICollection<Muzycy> MuzycyIdMuzyks { get; set; } = new List<Muzycy>();
}
