using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _ico;
    [SerializeField] private Button _button;

    public void Init(string name, Sprite sprite, List<UnityAction> actions)
    {
        _name.text = name;
        _ico.sprite = sprite;
        foreach (UnityAction action in actions)
        {
            _button.onClick.AddListener(action);
        }
    }
}
