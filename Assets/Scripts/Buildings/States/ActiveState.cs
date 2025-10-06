using System.Collections.Generic;
using UnityEngine;

public class ActiveState : BuildingState
{
    public ActiveState(Building building) : base(building)
    {
        building.GetComponentInChildren<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public override List<BuildingActionData> GetActionData()
    {
        List<BuildingActionData> actionData = new();
        if ( building.Level <  building.MaxLvl)
        {
            actionData.Add(new BuildingActionData(BuildingActionType.Upgrade,  Upgrade));
        }
        actionData.Add(new BuildingActionData(BuildingActionType.Destroy,  Destroy));
        return actionData;
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
         building.AddLevel();
         building.GetComponentInChildren<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
