using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// 새로운 Scene으로 전환할 때 RootCanvas는 해당 함수를 호출하여 현재 Scene의 Canvas들을 추적한다.
    /// </summary>
    /// <param name="rootCanvas"></param>
    public void RegisterRootCanvas(IUIComponent rootCanvas)
    {
        _rootCanvas = rootCanvas as RootUICanvas;
        _canvasTrace.Push(new List<IUIComponent> { rootCanvas });
    }

    /// <summary>
    /// Scene 전환 시 현재 Scene의 모든 Canvas들을 비워야 다음 Scene의 Root Canvas가 Stack의 가장 아래 위치
    /// </summary>
    /// <param name="rootCanvas"></param>
    public void UnregisterRootCanvas(IUIComponent rootCanvas)
    {
        while(_canvasTrace.Count > 0) _canvasTrace.Pop();
    }
    
    public void OpenChildrenCanvas(IUIComponent iuiComponent)
    {
        List<IUIComponent> thisCanvas = _canvasTrace.Peek();
        
        _canvasTrace.Push(iuiComponent.GetChildren());
        
        foreach (var subCanvas in thisCanvas)
        {
            subCanvas.Hide();
        }
        
        foreach (var nextCanvas in _canvasTrace.Peek())
        {
            nextCanvas.Show();
        }
    }

    /// <summary>
    /// 현재 UICanvas의 부모 UICanvas를 활성화 시킴
    /// </summary>
    public void CloseChildrenCanvas()
    {
        if (!ReferenceEquals(_rootCanvas, _canvasTrace.Peek()))
        {
            foreach (var subCanvas in _canvasTrace.Pop())
            {
                subCanvas.Hide();
            }
        
            foreach (var nextCanvas in _canvasTrace.Peek())
            {
                nextCanvas.Show();
            }
        }
        else
        {
            Debug.LogError("Root canvas can't be Close");
        }
    }
    
    /// <summary>
    /// 다른 Manager, Controller와 협업하기 위한 Method.
    /// </summary>
    /// <param name="command">해당 이벤트를 처리하기 위한 Command</param>
    public void ExecuteEvent(ICommand command)
    {
        command?.Execute();
    }
    
    public DataEventHandler dataEventHandler;

    /// <summary>
    /// 최상위의 UI Canvas
    /// </summary>
    private RootUICanvas _rootCanvas;
    
    //private IUIComponent uiCanvas;
    private readonly Stack<List<IUIComponent>> _canvasTrace = new();
}
