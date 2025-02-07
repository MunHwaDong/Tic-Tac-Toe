using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCommand : ICommand
{
    public bool Execute()
    {
        Debug.LogError("End Game Command!!!!!!!!!!!!!!!!!!");
        return true;
    }

    private readonly DataController _dataController = GameManager.Instance.DataController;
}
