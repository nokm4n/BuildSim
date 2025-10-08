using System;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
	public static GridBuildingSystem instance;

	[SerializeField] private int _height = 10;
	[SerializeField] private int _width = 10;
	[SerializeField] private float _cellSize = 10;
	[SerializeField] private LayerMask _layerMask;

	[SerializeField] private List<Building> _buildings = new List<Building>();

	private Building _selectedBuilding;
	private Grid<GridObject> _grid;
	private AbstractMode _mode;

	public event Action<Building> OnBuildingChanged;
	public event Action<bool> OnBuildMode;

	public Grid<GridObject> Grid => _grid;
	public List<Building> Buildings => _buildings;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		instance = this;

		CEvents.OnLoadSave += LoadBuildings;
		_grid = new Grid<GridObject>(_width, _height, _cellSize, Vector3.zero, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));
	}

	private void OnDisable()
	{
		CEvents.OnLoadSave -= LoadBuildings;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if(_mode != null)
			{
				_mode.Action();
				OnBuildMode?.Invoke(false);
			}
		}
	}

	/*public void SpawnBuilding()
	{
		bool canBuild = false;
		_grid.GetGridPos(GetMouseWorldPos(), out int x, out int z);

		if (_selectedBuilding == null) return;
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
		}
		canBuild = CanBuild(x, z, out List<GridObject> gridObjList);

		if (canBuild)
		{
			var building = Instantiate(_selectedBuilding, _grid.GetWorldPos(x, z), Quaternion.identity);
			building.CreateBuilding(_grid.GetWorldPos(x, z));
			foreach (var gridObject in gridObjList)
			{
				gridObject.SetBuilding(building);

			}
		}
	}

	public void DestroyBuilding()
	{
		_grid.GetGridPos(GetMouseWorldPos(), out int x, out int z);
		GridObject gridObj = _grid.GetObject(x, z);
		var building = gridObj.GetBuilding();
		if (building != null)
		{
			List<Vector2Int> gridPosList = building.GetGridPosList(new Vector2Int(x, z), Dir.Down);
			foreach (var gridPos in gridPosList)
			{
				var gridObject = _grid.GetObject(gridPos.x, gridPos.y);
				gridObj.FreeCell();
			}
			building.Destroy();
		}
	}*/

	public Vector3 GetMouseWorldPos()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out RaycastHit raycastHit, _layerMask))
		{
			return raycastHit.point;
		}
		return Vector3.zero;
	}

	public Vector3 GetSnappedPosition()
	{
		_grid.GetGridPos(GetMouseWorldPos(), out int x, out int z);
		return _grid.GetWorldPos(x, z);
	}

	public void SetBuilding(int index)
	{
		if (_buildings.Count > index)
		{
			_selectedBuilding = _buildings[index];
			OnBuildMode?.Invoke(true);
			OnBuildingChanged?.Invoke(_selectedBuilding);
			_mode = new BuildMode(_grid, _layerMask, _selectedBuilding);
		}
	}

	public void SetDestroyMode()
	{
		_mode = new DestroyMode(_grid, _layerMask, _selectedBuilding);
		OnBuildMode?.Invoke(false);
	}

	public bool CanBuild(int x, int z, out List<GridObject> gridObjList)
	{
		bool canBuild = false;
		List<Vector2Int> gridPosList = _selectedBuilding.GetGridPosList(new Vector2Int(x, z), Dir.Down);
		gridObjList = new List<GridObject>();
		foreach (var gridPos in gridPosList)
		{
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
		}
		return canBuild;
	}

	private void LoadBuildings(List<BuildingSaveData> saveData)
	{
		foreach (BuildingSaveData data in saveData)
		{
			foreach (Building buildingPrefab in _buildings)
			{
				if (buildingPrefab.Type == data.type)
				{
					_selectedBuilding = buildingPrefab;
					_grid.GetGridPos(data.origin, out int x, out int z);
					CanBuild(x, z, out List<GridObject> gridObjList);
					var building = MonoBehaviour.Instantiate(buildingPrefab, data.origin, Quaternion.identity);
					building.CreateBuilding(data.origin);
					CEvents.FireBuildingCreated(building);
					foreach (var gridObject in gridObjList)
					{
						gridObject.SetBuilding(building);

					}
				}
			}
		}
	}
}
