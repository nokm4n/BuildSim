using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMode
{
	protected LayerMask layerMask;
	protected Grid<GridObject> grid;
	protected Building selectedBuilding;

	public AbstractMode(Grid<GridObject> grid, LayerMask layerMask, Building selectedBuilding)
	{
		this.grid = grid;
		this.layerMask = layerMask;
		this.selectedBuilding = selectedBuilding;
	}

    public abstract void Action();

	protected Vector3 GetMouseWorldPos()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMask))
		{
			return raycastHit.point;
		}
		return Vector3.zero;
	}

	protected bool CanBuild(int x, int z, out List<GridObject> gridObjList)
	{
		bool canBuild = false;
		List<Vector2Int> gridPosList = selectedBuilding.GetGridPosList(new Vector2Int(x, z), Dir.Down);
		gridObjList = new List<GridObject>();
		foreach (var gridPos in gridPosList)
		{
			var gridObjects = grid.GetObject(gridPos.x, gridPos.y);
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
		}
		return canBuild;
	}
}
