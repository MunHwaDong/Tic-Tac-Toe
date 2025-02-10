using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public void Run()
    {
        ChangeState(new StartState(this));
    }
    
    public void ChangeState(IState newState)
    {
        _currentState?.ExitState();
        
        _currentState = newState;
        
        _currentState?.EnterState();
    }

    void Update()
    {
        _currentState?.UpdateState();
    }
    
    private IState _currentState;
}