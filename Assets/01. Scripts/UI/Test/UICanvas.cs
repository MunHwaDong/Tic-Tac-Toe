using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICanvas : MonoBehaviour, IUIComponent
{
    public void Init()
    {
        _children = GetComponentsInChildren<IUIComponent>().
            Where(c => !ReferenceEquals(c, this)).ToList();
        
        foreach (var child in _children)
        {
            child.Init();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        foreach (var child in _children)
        {
            child.Show();
        }
    }

    public void Hide()
    {
        foreach (var child in _children)
        {
            child.Hide();
        }
        
        gameObject.SetActive(false);
    }

    public List<IUIComponent> GetChildren()
    {
        return _children;
    }
    
    private List<IUIComponent> _children = new();
}
