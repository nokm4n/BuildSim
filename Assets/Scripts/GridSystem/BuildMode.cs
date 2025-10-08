using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : AbstractMode
{
	public BuildMode(Grid<GridObject> grid, LayerMask layerMask, Building selectedBuilding) : base(grid, layerMask, selectedBuilding)
	{
	}

	public override void Action()
	{
		SpawnBuilding();
	}

	private void SpawnBuilding()
	{
		bool canBuild = false;
		grid.GetGridPos(GetMouseWorldPos(), out int x, out int z);

		if (selectedBuilding == null) return;
		/*List<Vector2Int> gridPosList = _selectedBuilding.GetGridPosList(new Vector2Int(x, z), Dir.Down);
		List<GridObject> gridObjList = new List<GridObject>();
		foreach (var gridPos in gridPosList)
		{
			Debug.Log("Test" + gridPos.x + gridPos.y);
			var gridObjects = _grid.GetObject(gridPos.x, gridPos.y);
			if (gridObjects != null)
			{
				if (gridObjects.CanBuild())
				{
					canBuild = true;
					gridObjList.Add(gridObjects);
				}
				else
				{
					canBuild = false;
					break;
				}
			}
			else
			{
				canBuild = false;
				break;
			}
		}*/
		canBuild = CanBuild(x, z, out List<GridObject> gridObjList);

		if (canBuild)
		{
			var building = MonoBehaviour.Instantiate(selectedBuilding, grid.GetWorldPos(x, z), Quaternion.identity);
			building.CreateBuilding(grid.GetWorldPos(x, z));
			CEvents.FireBuildingCreated(building);
			foreach (var gridObject in gridObjList)
			{
				gridObject.SetBuilding(building);

			}
		}
	}
}
