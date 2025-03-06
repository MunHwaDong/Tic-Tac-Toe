using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[
    RequireComponent(typeof(Button))
]
public class OnlineMatchButton : UserEventComponent
{
    public override void Init()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(EventMethod);
        }
        
        CastingChildren();
    }

    public override void EventMethod()
    {
        TempUIManager.Instance.OpenChildrenCanvas(this);
    }

    public override List<IUIComponent> GetChildren()
    {
        return childrenComponent;
    }
}
