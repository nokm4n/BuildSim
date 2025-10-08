using System;
using System.Collections.Generic;
using UnityEngine;

public class CEvents : MonoBehaviour
{
	public static event Action<Building> OnBuildingCreated;
	public static void FireBuildingCreated(Building building) => OnBuildingCreated?.Invoke(building);

	public static event Action<Building> OnBuildingDestroyed;
	public static void FireBuildingDestroyed(Building building) => OnBuildingDestroyed?.Invoke(building);

	public static event Action<List<BuildingSaveData>> OnLoadSave;
	public static void FireLoadSave(List<BuildingSaveData> saveData) => OnLoadSave?.Invoke(saveData);
}