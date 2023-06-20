using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class Muzycy
{
    public int IdMuzyk { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string? Pseudonim { get; set; }

    public virtual ICollection<Utwory> UtworyIdUtwors { get; set; } = new List<Utwory>();
}
