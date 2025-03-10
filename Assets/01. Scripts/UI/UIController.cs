using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public delegate void UpdateUIDelegate(GameData gameData);
    public event UpdateUIDelegate updateUI;
    
    public void Init()
    {
        //GameManager.Instance.DataController.OnChangeGameData += UpdateUI;
        
        var uiElements = GetComponentsInChildren<IUIElement>();

        foreach (var uiElement in uiElements)
        {
            uiElement.Init(this);
        }
    }

    private void UpdateUI(GameData gameData)
    {
        foreach (var uiElement in _elements)
        {
            uiElement.UpdateElement(gameData);
        }
    }
    
    private List<IUIElement> _elements = new();
}
