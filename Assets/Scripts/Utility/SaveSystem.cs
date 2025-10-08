using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	private static string _savePath;
	private List<Building> _buildings = new List<Building>();
	//private List<BuildingSaveData> _buildingsSaveData = new List<BuildingSaveData>();

	private void OnEnable()
	{
		_savePath = Path.Combine(Application.persistentDataPath, "buildings.json");
		CEvents.OnBuildingCreated += OnBuildingCreated;
		CEvents.OnBuildingDestroyed += OnBuildingDestroyed;

		CEvents.FireLoadSave(Load());
	}

	private void OnDisable()
	{
		CEvents.OnBuildingCreated -= OnBuildingCreated;
		CEvents.OnBuildingDestroyed -= OnBuildingDestroyed;
	}

	/*private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Save();
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			CEvents.FireLoadSave(Load());
		}
	}*/

	public void Save()
	{
		List<BuildingSaveData> buildingsSaveData = new List<BuildingSaveData>();
		foreach (Building building in _buildings)
		{
			BuildingSaveData saveData = new BuildingSaveData();
			saveData.origin = building.Origin;
			saveData.type = building.Type;
			buildingsSaveData.Add(saveData);
		}

		string json = JsonHelper.ToJson(buildingsSaveData, true);
		File.WriteAllText(_savePath, json);
		Debug.Log($"✅ Saved {buildingsSaveData.Count} buildings to {_savePath}");
	}

	public List<BuildingSaveData> Load()
	{
		if (!File.Exists(_savePath))
			return new List<BuildingSaveData>();

		string json = File.ReadAllText(_savePath);
		List<BuildingSaveData> array = JsonHelper.FromJson<BuildingSaveData>(json);
		return new List<BuildingSaveData>(array);
	}

	private void OnBuildingCreated(Building building)
	{
		_buildings.Add(building);

	}

	private void OnBuildingDestroyed(Building building)
	{
		_buildings.Remove(building);
	}
}

[System.Serializable]
public struct BuildingSaveData
{
	public Vector3 origin;
	public BuildingType type;
}
public static class JsonHelper
{
	[System.Serializable]
	private class Wrapper<T>
	{
		public List<T> Items;
	}

	public static string ToJson<T>(List<T> list, bool prettyPrint = false)
	{
		Wrapper<T> wrapper = new Wrapper<T> { Items = list };
		return JsonUtility.ToJson(wrapper, prettyPrint);
	}

	public static List<T> FromJson<T>(string json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper?.Items ?? new List<T>();
	}
}