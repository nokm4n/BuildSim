using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	private string _filePath = "myNewFile.json";

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Save();
		}
	}

	public void Save()
	{
		foreach (GridObject gridObj in GridBuildingSystem.instance.Grid.GridArray)
		{
			Debug.Log(gridObj.ToString());
			if(gridObj.GetBuilding() != null)
				Debug.Log(gridObj.GetBuilding().Type);
		}
	}
}
