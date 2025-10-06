using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour
{
    //[SerializeField] private GridBuildingSystem _gridBuildingSystem;
	[SerializeField] private Material _ghostMatGreen, _ghostMatRed;
	private Building _building;
	private bool _isBuilding = false;

	private void Awake()
	{
		GridBuildingSystem.instance.OnBuildingChanged += UpdateVisual;
		GridBuildingSystem.instance.OnBuildMode += SwitchMode;
	}

	private void OnDisable()
	{
		GridBuildingSystem.instance.OnBuildingChanged -= UpdateVisual;
		GridBuildingSystem.instance.OnBuildMode -= SwitchMode;
	}

	private void LateUpdate()
	{
		if (!_isBuilding || _building == null) return;

		Vector3 targetPos = GridBuildingSystem.instance.GetSnappedPosition();
		//targetPos.y = .5f;
		bool canBuild = false;
		GridBuildingSystem.instance.Grid.GetGridPos(GridBuildingSystem.instance.GetMouseWorldPos(), out int x, out int z);

		canBuild = GridBuildingSystem.instance.CanBuild(x, z, out List<GridObject> gridObjList);
		/*List<Vector2Int> gridPosList = _building.GetGridPosList(new Vector2Int(x, z), Dir.Down);
		List<GridObject> gridObjList = new List<GridObject>();
		foreach (var gridPos in gridPosList)
		{
			Debug.Log("Test" + gridPos.x + gridPos.y);
			var gridObjects = _gridBuildingSystem.Grid.GetObject(gridPos.x, gridPos.y);
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
		SetMat(canBuild);
		_building.transform.position = Vector3.Lerp(_building.transform.position, targetPos, Time.deltaTime * 15f);
	}

	private void SwitchMode(bool mode)
	{
		_isBuilding = mode;

		if(!mode)
		{
			if(_building != null)
			{
				Destroy(_building.gameObject);
				_building = null;
			}
		}
	}

	private void UpdateVisual(Building building)
	{
		if(_building != null)
		{
			Destroy(_building.gameObject);
			_building = null;
		}
		_building = Instantiate(building, GridBuildingSystem.instance.GetSnappedPosition(), Quaternion.identity);
		foreach(var render in _building.Renderers)
		{
			render.material = _ghostMatGreen;
		}
	}

	private void SetMat(bool canBuild)
	{
		if (canBuild)
		{
			foreach (var render in _building.Renderers)
			{
				render.material = _ghostMatGreen;
			}
		}
		else
		{
			foreach (var render in _building.Renderers)
			{
				render.material = _ghostMatRed;
			}
		}
	}
}
