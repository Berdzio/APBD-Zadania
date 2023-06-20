using System;
using System.Collections.Generic;

namespace cwiczenia8.Entites;

public partial class Action
{
    public int IdAction { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool NeedSpecialEquipment { get; set; }

    public virtual ICollection<FireTruckAction> FireTruckActions { get; set; } = new List<FireTruckAction>();

    public virtual ICollection<Firefighter> IdFirefighters { get; set; } = new List<Firefighter>();
}
