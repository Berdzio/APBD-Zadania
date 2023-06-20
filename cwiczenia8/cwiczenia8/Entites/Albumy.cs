using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class Albumy
{
    public int IdAlbumu { get; set; }

    public string NazwaAlbumu { get; set; } = null!;

    public DateTime DataWydania { get; set; }

    public int IdWytwornia { get; set; }

    public virtual Wytwornie IdWytworniaNavigation { get; set; } = null!;

    public virtual ICollection<Utwory> Utwories { get; set; } = new List<Utwory>();
}
