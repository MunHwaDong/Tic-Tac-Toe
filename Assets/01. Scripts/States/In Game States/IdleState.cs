using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public IdleState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void EnterState()
    {
        _actions.AddCommand(new IdleCommand());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateState()
    {
        if(_actions.ExcuteCommands() is true)
            _stateMachine.ChangeState(new OnDropState(_stateMachine));
    }

    public void ExitState()
    {
        Debug.Log("Exiting IdleState");
    }
    
    private readonly StateMachine _stateMachine;
    private readonly Invoker _actions = new();
}