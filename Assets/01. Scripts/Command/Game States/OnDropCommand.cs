using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDropCommand : ICommand
{
    /*
     * 1. 입력 받은 Cell 데이터가 null인지
     * 2. null이 아니라면, 해당 Cell에 아무런 Marker가 없는지
     * 3. 아무런 Marker가 없다면, 현재 누구의 턴인지 정보를 받아온다.
     * 4. 현재 턴 정보와, Marker 스프라이트를 Grid에게 넘겨줘서 해당 Cell을 칠한다.
     * 5. 현재 턴의 플레이어가 이겼는지 판단한다.
     *  5-1. 현재 턴의 플레이어가 이겼다면, EndGame 상태로 전환한다.
     * 6. 게임 종료 조건을 만족했는지 검사한다.
     *  6-1. EndGame 상태로 전환한다.
     * 7. "5번"과 "6번" != false라면 다시 Idle 상태로 돌아간다.
     */
    public bool Execute()
    {
        if(_dataController.OnDropMarker() is false)
            return false;

        if (_dataController.CheckForWin() is false)
        {
            _dataController.ChangeTurn();
            return false;
        }

        if (_dataController.isGridFull() is true)
            return true;

        return true;
    }

    private readonly DataController _dataController = GameManager.Instance.DataController;
}
