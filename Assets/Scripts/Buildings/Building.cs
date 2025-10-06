using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] private float _cost;
    [SerializeField] private int _maxLvl = 3;
    [SerializeField] private int _width = 1, _lenght = 1;
    [SerializeField] private List<Renderer> _render = new List<Renderer>();
    [SerializeField] private Sprite _ico;
    [SerializeField] private BuildingType _type;

    public event Action OnDestroyBuilding;
    public float Cost =>  _cost;
    public int MaxLvl =>  _maxLvl;
    public int Level =>  level;
    public int Width => _width;
    public int Lenght => _lenght;
    public Sprite Ico => _ico;
    public BuildingType Type => _type;

    protected int level = 0;
    protected BuildingState curentState;
    private BuildingStateType _buildingStateType;
    
    public List<Renderer> Renderers => _render;

    public List<Vector2Int> GetGridPosList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>();
        switch (dir)
        {
            case Dir.Down:
            case Dir.Up:
                for (int i = 0; i < _width; i++)
                {
                    for(int j = 0; j < _lenght; j++)
                    {
                        gridPosList.Add(offset + new Vector2Int(i, j));
                    }
                }
                break;
                case Dir.Left:
                case Dir.Right:
				for (int i = 0; i < _lenght; i++)
				{
					for (int j = 0; j < _width; j++)
					{
						gridPosList.Add(offset + new Vector2Int(i, j));
					}
				}
				break;
}
        return gridPosList;
    }

    public virtual void CreateBuilding(Vector3 origin)
    {
        SetState(BuildingStateType.NotConstructed);
    }

    public List<BuildingActionData> GetActionData()
    {
        return  curentState.GetActionData();
    }

    public void Destroy()
    {
        OnDestroyBuilding?.Invoke();
        Destroy( gameObject);
    }

    public void SetLevel(int level)
    {
        if (level < 0 || level >  _maxLvl)
        {
            Debug.Log("Something wrong! Level can't be " + level);
            return;
        }

         this.level = level;
    }

    public void AddLevel()
    {
        if ( level <  _maxLvl)
        {
             level++;
        }
        else
        {
            Debug.Log("Level is already MAX");
        }
    }

    public void SetState(BuildingStateType buildingStateType)
    {
        BuildingState newState = null;
        
        switch (buildingStateType)
        {
            case BuildingStateType.NotConstructed:
                newState = new NotConstructedState(this);
                break;
            case BuildingStateType.Active:
                newState = new ActiveState(this);
                break;
            case BuildingStateType.Broken:
                newState = new BrokenState(this);
                break;
            default:
                newState = null;
                break;
        }
         curentState = newState;
         _buildingStateType = buildingStateType;
    }
}

public enum Dir
{
    Down,
    Up,
    Right,
    Left
}

public enum BuildingType
{
    Shop,
    Canteen,
    CanteenBig
}