using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public delegate void UpdateUI(GameData gameData);
    public event UpdateUI updateUI;
    
    public void Init()
    {
        var uiElements = GetComponentsInChildren<IUIElement>();

        foreach (var uiElement in uiElements)
        {
            uiElement.Init(this);
        }
    }
    
    private List<IUIElement> _elements = new();
}
