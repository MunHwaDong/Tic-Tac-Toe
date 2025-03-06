using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 1)]
public class GameData : ScriptableObject
{
    //Marker Sprite & Cell Prefab Data
    public Sprite player1Marker;
    public Sprite player2Marker;
    public Sprite emptySprite;
    
    public GameObject cellPrefab;

    //Play Data
    public Turn currentTurn;
    public Turn winner;
    
    public int player1WinPoint;
    public int player2WinPoint;

    public delegate void OnChangedWinner(GameData gameData);
    public event OnChangedWinner onChangedWinner;

    public delegate void OnChangedPlayersPoint(GameData gameData);
    public event OnChangedPlayersPoint onChangedPlayersPoint;

    public void ChangePlayersPoint()
    {
        _ = (winner == Turn.PLAYER1) ? player1WinPoint++ : player2WinPoint++;
        onChangedPlayersPoint?.Invoke(this);
    }
}