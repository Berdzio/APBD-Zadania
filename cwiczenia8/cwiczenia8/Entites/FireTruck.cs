using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class FireTruck
{
    public int IdFiretruck { get; set; }

    public string OperationNumber { get; set; } = null!;

    public bool SpecialEquipment { get; set; }

    public virtual ICollection<FireTruckAction> FireTruckActions { get; set; } = new List<FireTruckAction>();
}
