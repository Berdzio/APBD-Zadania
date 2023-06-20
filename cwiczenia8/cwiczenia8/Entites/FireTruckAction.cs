using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class FireTruckAction
{
    public int IdFiretruck { get; set; }

    public int IdAction { get; set; }

    public DateTime AssignmentDate { get; set; }

    public virtual Action IdActionNavigation { get; set; } = null!;

    public virtual FireTruck IdFiretruckNavigation { get; set; } = null!;
}
