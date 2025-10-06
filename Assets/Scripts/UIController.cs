using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //[SerializeField] private UIDocument _uiDocument;
    //[SerializeField] private GridBuildingSystem _gridbuildingSystem;

    [SerializeField] private Button _build;
    [SerializeField] private Button _destroy;

    [SerializeField] private BuildingButton _buildingButton;
    [SerializeField] private GameObject _buildContainer;

    private List<BuildingButton> _buildingsButtons = new List<BuildingButton>();
    // private VisualElement _root;

    private void Start()
    {
        _build.onClick.AddListener(() => SwitchBuildContainer());
		_destroy.onClick.AddListener(() => GridBuildingSystem.instance.SetDestroyMode());
		_destroy.onClick.AddListener(() => TurnOffContainer());

		for (int i = 0; i < GridBuildingSystem.instance.Buildings.Count; i++)
		{
            int temp = i;
			var button = Instantiate(_buildingButton, _buildContainer.transform);
            List<UnityAction> actions = new List<UnityAction>();
            actions.Add(() => GridBuildingSystem.instance.SetBuilding(temp));
            actions.Add(() => TurnOffContainer());
            button.Init(GridBuildingSystem.instance.Buildings[i].name, GridBuildingSystem.instance.Buildings[i].Ico, actions);
			//button.onClick.AddListener(() => _gridbuildingSystem.SetBuilding(temp));
			//button.onClick.AddListener(() => TurnOffContainer());
			_buildingsButtons.Add(button);
		}
	}

    private void SwitchBuildContainer()
    {
        _buildContainer.SetActive(!_buildContainer.activeSelf);        
    }

    private void TurnOffContainer()
    {
        _buildContainer.SetActive(false);
	}
}
