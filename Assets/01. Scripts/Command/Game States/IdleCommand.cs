using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCommand : ICommand
{
    public bool Execute()
    {
        if (_gameManager.GetInput())
            _raycastHit = _gameManager.TryRaycastHit(nameof(Cell));
        else
            return false;
        
        if (_raycastHit.collider is null) 
            return false;
        else
            return _dataController.AddData<Cell>(_raycastHit.collider.TryGetComponent(out Cell cell) ? cell : null);
    }
    
    private RaycastHit2D _raycastHit;
    
    private readonly DataController _dataController = GameManager.Instance.DataController;
    private readonly GameManager _gameManager = GameManager.Instance;
}
