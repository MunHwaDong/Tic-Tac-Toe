using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultWindow : DataFriendlyComponent
{
    public override void Init()
    {
        UIManager.Instance.dataEventHandler.onUpdatePlayersPoint += OnChangedDataEvent;
        EventBus.RegisterEvent(GameEvent.GAME_END, EventMethod);
        
        CastingChildren();
    }

    public override void OnChangedDataEvent(GameData data)
    {
        if (data.winner == Turn.PLAYER1)
        {
            winText.text = "Player 1 Wins!";
        }
        else
        {
            winText.text = "Player 2 Wins!";
        }

        winText.text += " 계속 하시겠습니까?";
    }

    private void EventMethod()
    {
        UIManager.Instance.OpenChildrenCanvas(this);
    }

    public override List<IUIComponent> GetChildren()
    {
        return childrenComponent;
    }
    
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private bool isThisCanvasHide = false;
}
