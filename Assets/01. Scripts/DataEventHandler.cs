using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEventHandler
{
    public delegate void OnUpdatePlayersPoint(GameData data);
    public event OnUpdatePlayersPoint onUpdatePlayersPoint;

    public delegate void OnUpdateTurn(GameData data);
    public event OnUpdateTurn onUpdateTurn;

    public DataEventHandler(GameData data)
    {
        data.onChangedPlayersPoint += UpdatePlayersPoint;
        data.onChangedTurn += UpdateTurn;
    }

    public void UpdatePlayersPoint(GameData data)
    {
        onUpdatePlayersPoint?.Invoke(data);
    }

    public void UpdateTurn(GameData data)
    {
        onUpdateTurn?.Invoke(data);
    }
}
