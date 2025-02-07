using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[
    RequireComponent(typeof(StateMachine))
]
public class GameManager : Singleton<GameManager>
{
    public bool GetInput()
    {
        return Input.GetMouseButtonUp(0);
    }

    public RaycastHit2D TryRaycastHit()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        
        return Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
    }
    
    public RaycastHit2D TryRaycastHit(string targetLayer)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer(targetLayer));
    }

    void Start()
    {
        Init();
        _stateMachine.Run();
    }

    void Init()
    {
        TryGetComponent(out _stateMachine);
        DataController.Init();
        UIController.Init();
    }

    private StateMachine _stateMachine;
    private DataController _dataController;
    private UIController _uiController;
    
    public DataController DataController => (_dataController ??= FindObjectOfType<DataController>());
    public UIController UIController => (_uiController ??= FindObjectOfType<UIController>());
}
