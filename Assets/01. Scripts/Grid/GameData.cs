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
    
    public int player1WinCounter;
    public int player2WinCounter;
}