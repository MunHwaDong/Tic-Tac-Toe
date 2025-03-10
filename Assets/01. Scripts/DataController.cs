using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataController : MonoBehaviour
{
    public void Init()
    {
        _gameData = Addressables.LoadAssetAsync<GameData>("Assets/02. Prefabs/GameData/GameData.asset").WaitForCompletion();
        _grid ??= FindObjectOfType<Grid>();
        _grid.Init(_gameData);
    }
    
    public bool OnDropMarker()
    {
        if (_data.ContainsKey(nameof(Cell)) is false || _data[nameof(Cell)] is null)
            return false;

        if (_data[nameof(Cell)] is Cell selectCell && selectCell.Marker == _gameData.emptySprite)
            return _grid.TryMarkingOnCell(selectCell._coordinate);

        return false;
    }

    public void ChangeTurn()
    {
        _gameData.currentTurn = _gameData.currentTurn == Turn.PLAYER1 ? Turn.PLAYER2 : Turn.PLAYER1;
    }
    
    public bool CheckForWin()
    {
        //clock-wise (n -> ne -> e -> se)
        //row
        int[] dy = { 1, 1, 0, -1};
        //col
        int[] dx = { 0, 1, 1, 1};

        var cell = _data[nameof(Cell)] as Cell;

        int row = cell._coordinate.Item1;
        int col = cell._coordinate.Item2;
        
        for (int dir = 0; dir < dy.Length; dir++)
        {
            int count = CheckWinRecursive(row, col, dy[dir], dx[dir], 0, cell) +
                        CheckWinRecursive(row, col, -dy[dir], -dx[dir], 0, cell) - 1;
            
            if (count == 3)
                return true;
        }

        return false;
    }

    private int CheckWinRecursive(int row, int col, int dy, int dx, int count, Cell curPlayerSelect)
    {
        if (_grid[row, col] is null)
            return count;
        if (_grid[row, col].Marker != curPlayerSelect.Marker)
            return count;

        return CheckWinRecursive(row + dy, col + dx, dy, dx, count + 1, curPlayerSelect);
    }

    public bool AddData<T>(T data)
    {
        string typeName = typeof(T).Name;

        if (data is null)
            return false;
        else if (_data.ContainsKey(typeName))
        {
            _data[typeName] = data;
        }
        else
        {
            _data.Add(typeName, data);
        }

        return true;
    }
    
    private Grid _grid;
    private GameData _gameData;
    private IDictionary<string, object> _data = new Dictionary<string, object>();
}
