using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[
    RequireComponent(typeof(StateMachine))
]
public class GameManager : Singleton<GameManager>
{
    private new void Awake()
    {
        Init();
        _stateMachine.Run();
    }
    
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

    void Init()
    {
        if (TryGetComponent(out _stateMachine) is false)
        {
            _stateMachine = gameObject.AddComponent<StateMachine>();
        }
    }

    private StateMachine _stateMachine;
    private DataController _dataController;
    
    public DataController DataController => (_dataController = FindObjectOfType<DataController>());
}