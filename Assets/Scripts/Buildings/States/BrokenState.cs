using System.Collections.Generic;
using UnityEngine;

public class BrokenState : BuildingState
{
    public BrokenState(Building building) : base(building)
    {
    }

    public override List<BuildingActionData> GetActionData()
    {
        return new List<BuildingActionData>()
        {
            new BuildingActionData(BuildingActionType.Repair,  Repair),
            new BuildingActionData(BuildingActionType.Destroy,  Destroy)
        };
    }

    public override void Repair()
    {
         building.SetState(BuildingStateType.Active);
    }

    public override void Destroy()
    {
         building.Destroy();
    }

    public override void Upgrade()
    {
        
    }
}
