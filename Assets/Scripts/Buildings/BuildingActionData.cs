using UnityEngine;
using UnityEngine.Events;

public class BuildingActionData : MonoBehaviour
{
    public readonly BuildingActionType BuildingActionType;
    public readonly UnityAction Callback;

    public BuildingActionData(BuildingActionType buildingActionType, UnityAction callback)
    {
         BuildingActionType = buildingActionType;
         Callback = callback;
    }
}
