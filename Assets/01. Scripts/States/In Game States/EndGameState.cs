using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : IState
{
    public EndGameState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void EnterState()
    {
        _actions.AddCommand(new EndGameCommand());
    }

    public void UpdateState()
    {
        _actions.ExcuteCommands();
    }

    public void ExitState()
    {

    }

    private readonly StateMachine _stateMachine;
    private readonly Invoker _actions = new();
}
