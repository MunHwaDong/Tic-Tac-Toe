using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDropState : IState
{
    public OnDropState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void EnterState()
    {
        _actions.AddCommand(new OnDropCommand());

        if (_actions.ExcuteCommands() is false)
        {
            _stateMachine.ChangeState(new IdleState(_stateMachine));
        }
        //Victory case
        else
            _stateMachine.ChangeState(new EndGameState(_stateMachine));
    }

    public void UpdateState()
    {
        //do nothing at Unity Update Loop
    }

    public void ExitState()
    {

    }

    private readonly StateMachine _stateMachine;
    private readonly Invoker _actions = new();
}
