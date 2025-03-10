using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[
    RequireComponent(typeof(TextMeshPro))
]
public class PlayersPanel : DataFriendlyComponent
{
    public override void Init()
    {
        UIManager.Instance.dataEventHandler.onUpdatePlayersPoint += OnChangedDataEvent;
    }

    public override void OnChangedDataEvent(GameData data)
    {
        player1PointText.text = data.player1WinPoint.ToString();
        player2PointText.text = data.player2WinPoint.ToString();
    }

    [SerializeField] private TextMeshProUGUI player1PointText;
    [SerializeField] private TextMeshProUGUI player2PointText;
}