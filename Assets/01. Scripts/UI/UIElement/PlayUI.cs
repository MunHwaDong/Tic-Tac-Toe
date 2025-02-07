using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : IUIElement
{
    public override void UpdateElement()
    {
        
    }

    private List<IUIElement> _children = new();
}
