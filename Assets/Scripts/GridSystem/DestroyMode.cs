using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMode : AbstractMode
{
	public DestroyMode(Grid<GridObject> grid, LayerMask layerMask, Building selectedBuilding) : base(grid, layerMask, selectedBuilding)
	{
	}

	public override void Action()
	{
		DestroyBuilding();
	}

	public void DestroyBuilding()
	{
		grid.GetGridPos(GetMouseWorldPos(), out int x, out int z);
		GridObject gridObj = grid.GetObject(x, z);
		if (gridObj == null) return;
		var building = gridObj.GetBuilding();
		if (building != null)
		{
			CEvents.FireBuildingDestroyed(building);
			List<Vector2Int> gridPosList = building.GetGridPosList(new Vector2Int(x, z), Dir.Down);
			foreach (var gridPos in gridPosList)
			{
				var gridObject = grid.GetObject(gridPos.x, gridPos.y);
				gridObj.FreeCell();
			}
			building.Destroy();
		}
	}
}
