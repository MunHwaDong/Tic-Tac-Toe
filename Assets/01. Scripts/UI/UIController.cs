using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public void Init()
    {
        var canvases = GetComponentsInChildren<Canvas>();

        foreach (var _canvas in canvases)
        {
            _elements.Add(nameof(_canvas.gameObject), _canvas.gameObject.GetComponent<IUIElement>());
        }
        
        Debug.Log(_elements.Count);
        foreach (var elementsKey in _elements.Keys)
        {
            Debug.Log(elementsKey);
        }
    }
    
    private readonly IDictionary<string, IUIElement> _elements = new Dictionary<string, IUIElement>();
}
