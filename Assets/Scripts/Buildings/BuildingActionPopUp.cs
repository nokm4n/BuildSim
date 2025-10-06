using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingActionPopUp : MonoBehaviour
{
    [SerializeField] private RectTransform _root;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Button _destroyButton;

   /* private void OnEnable()
    {
        CEvents.OnCellClicked +=  ShowPopUp;
    }

    private void OnDisable()
    {
        CEvents.OnCellClicked -=  ShowPopUp;
    }*/

    public void ShowPopup(List<BuildingActionData> towerData)
    {
         Hide();

         _root.transform.position = Input.mousePosition;
         _root.gameObject.SetActive(true);

        foreach (BuildingActionData actionData in towerData)
        {
            Button button =  GetButtonByType(actionData.BuildingActionType);
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(actionData.Callback);
            button.onClick.AddListener( Hide);
        }
    }

    public void Hide()
    {
         _root.gameObject.SetActive(false);
         _upgradeButton.gameObject.SetActive(false);
         _repairButton.gameObject.SetActive(false);
         _destroyButton.gameObject.SetActive(false);
    }

    private Button GetButtonByType(BuildingActionType towerActionType)
    {
        switch (towerActionType)
        {
            case BuildingActionType.Upgrade:
                return  _upgradeButton;
            case BuildingActionType.Repair:
                return  _repairButton;
            case BuildingActionType.Destroy:
                return  _destroyButton;
            default:
                Debug.LogError("Not all enums for TowerActionPopup covered! Unknown type " + towerActionType);
                return null;
        }
    }

    private void ShowPopUp(Building building)
    {
         ShowPopup(building.GetActionData());
    }
}
