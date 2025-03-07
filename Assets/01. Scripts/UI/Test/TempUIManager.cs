using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TempUIManager : Singleton<TempUIManager>
{
    void Start()
    {
        //Init();

        SceneLoader.OnSceneLoadingStarts += Init;
    }

    public void Init(SceneId sceneId)
    {
        if (rootCanvas.TryGetComponent(out IUIComponent iuiComponent))
        {
            _canvasTrace.Push(new List<IUIComponent>() { iuiComponent });

            iuiComponent.Init();
        }

        foreach (var subCanvas in GetComponentsInChildren<UICanvas>()
                     .Where(c => !ReferenceEquals(c, iuiComponent)))
        {
            subCanvas.Init();
            subCanvas.Hide();
        }
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
        if (!ReferenceEquals(rootCanvas, _canvasTrace.Peek()))
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

    /// <summary>
    /// 최상위의 UI Canvas
    /// </summary>
    [SerializeField] private GameObject rootCanvas;
    
    //private IUIComponent uiCanvas;
    private readonly Stack<List<IUIComponent>> _canvasTrace = new();
}
