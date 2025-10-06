using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingState
{
    protected Building building;

    public BuildingState(Building building)
    {
         building = building;
    }

    public abstract List<BuildingActionData> GetActionData();
    public abstract void Upgrade();
    public abstract void Repair();
    public abstract void Destroy();
}
