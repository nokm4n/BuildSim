using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject> : MonoBehaviour
{
	public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
	public class OnGridObjectChangedEventArgs : EventArgs
	{
		public int x;
		public int y;
	}

	private int _width, _height;
	private float _cellSize;
	private Vector3 _origPos;
	private TGridObject[,] _gridArray;

	public TGridObject[,] GridArray => _gridArray;

	public Grid(int width, int height, float cellSize, Vector3 origPos, Func<Grid<TGridObject>, int, int, TGridObject> createGridObj)
	{
		_width = width;
		_height = height;
		_cellSize = cellSize;
		_origPos = origPos;

		_gridArray = new TGridObject[width, height];

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				_gridArray[i, j] = createGridObj(this, i, j);
			}
		}

		/*if(true)
		{
			TextMesh[,] debugTextArray = new TextMesh[width, height];

			for(int i = 0; i < width; i++)
			{
				for(int j = 0;j < height; j++)
				{
					debugTextArray[i, j] = UtilsClass.
				}
			}
		}*/
	}

	public void GetGridPos(Vector3 origPos, out int x, out int z)
	{
		x = Mathf.FloorToInt(origPos.x/ _cellSize);
		z = Mathf.FloorToInt(origPos.z/ _cellSize);
	}

	public Vector3 GetWorldPos(int x, int z)
	{
		return new Vector3(x, 0, z) * _cellSize;
	}

	public TGridObject GetObject(int x, int z)
	{
		if(x >= 0 && z >= 0 && x < _width && z < _height)
		{
			return _gridArray[x, z];
		}
		else
		{
			return default;
		}
	}
}
