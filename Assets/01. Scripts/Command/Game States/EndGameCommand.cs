using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCommand : ICommand
{
    public bool Execute()
    {
        EventBus.Publish(GameEvent.GAME_END);
        
        return true;
    }

    private readonly DataController _dataController = GameManager.Instance.DataController;
    private readonly UIController _uiController = GameManager.Instance.UIController;
}
