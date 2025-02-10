using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    public StartState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void EnterState()
    {
        EventBus.Publish(GameEvent.GAME_START);
        
        _stateMachine.ChangeState(new IdleState(_stateMachine));
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }
    
    private readonly StateMachine _stateMachine;
    private readonly Invoker _actions = new Invoker();
}
