using System.Collections.Generic;
using UnityEngine;

public class NotConstructedState : BuildingState
{
    public NotConstructedState(Building building) : base(building)
    {
    }

    public override List<BuildingActionData> GetActionData()
    {
        return new List<BuildingActionData>()
        {
            new BuildingActionData(BuildingActionType.Upgrade,  Upgrade),
            new BuildingActionData(BuildingActionType.Destroy,  Destroy)
        };
    }

    public override void Repair()
    {
        
    }

    public override void Destroy()
    {
         building.Destroy();
    }

    public override void Upgrade()
    {
         building.SetLevel(1);
         building.SetState(BuildingStateType.Active);
    }
}
