using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStrategy : MonoBehaviour
{
    protected OpponentController opponentController;

    public abstract void DoAction();
}
