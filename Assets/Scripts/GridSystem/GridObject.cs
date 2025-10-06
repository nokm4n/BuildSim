using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private Grid<GridObject> _grid;
    private int _x, _y;
	private Building _building = null;

	public bool CanBuild() => _building == null;

    public GridObject(Grid<GridObject> grid, int x, int y)
	{
		_grid = grid;
		_x = x;
		_y = y;
	}

	public void SetBuilding(Building building)
	{
		_building = building;
	}

	public void FreeCell()
	{
		_building = null;
	}

	public Building GetBuilding()
	{
		return _building;
	}
}
