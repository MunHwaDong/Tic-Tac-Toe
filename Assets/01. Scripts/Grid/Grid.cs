using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //operator '[]' overloading
    public Cell this[int row, int col]
    {
        get
        {
            if (row < 0 || row > GridSize - 1 || col < 0 || col > GridSize - 1)
                return null;
            else
                return _grid[(row, col)];
        }
    }
    
    public bool TryMarkingOnCell((int, int) coordi)
    {
        if (_grid[coordi].Marker is not null)
        {
            if (_gameData.currentTurn == Turn.PLAYER1)
            {
                _grid[coordi].Marker = _gameData.player1Marker;
            }
            else
            {
                _grid[coordi].Marker = _gameData.player2Marker;
            }

            _remainCells--;
            return true;
        }
        
        return false;
    }

    public bool TryUnmarkingOnCell((int, int) coordi)
    {
        if (_grid[coordi].Marker != _gameData.emptySprite)
        {
            _grid[coordi].Marker = _gameData.emptySprite;
            _remainCells++;
            return true;
        }

        return false;
    }

    public void Init(GameData gameData)
    {
        _gameData = gameData;
        _remainCells = GridSize * GridSize;
        _gridSprite = GetComponent<SpriteRenderer>();
        
        _grid = new Dictionary<(int, int), Cell>();

        for (int row = -1; row < GridSize - 1; row++)
        {
            for (int col = -1; col < GridSize - 1; col++)
            {
                Vector3 pos = new Vector3((col * _gridSprite.bounds.size.x) / GridSize,
                                        (row * _gridSprite.bounds.size.y) / GridSize + _cameraOffsetY, 0);
                
                GameObject obj = Instantiate(gameData.cellPrefab, pos, Quaternion.identity);
                obj.transform.SetParent(transform);
                
                Cell cell = obj.AddComponent<Cell>();
                cell.Init((row + 1, col + 1), gameData.emptySprite);
                _grid.Add((row + 1, col + 1), cell);
            }
        }
    }

    public void Clear()
    {
        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                _grid[(row, col)].EraseMarker(_gameData.emptySprite);
            }
        }
    }

    private int _remainCells = 0;
    public bool RemainCells => _remainCells < GridSize * GridSize;
    
    private const int GridSize = 3;
    private readonly float _cameraOffsetY = -1f;
    
    private GameData _gameData;
    private SpriteRenderer _gridSprite;
    private IDictionary<(int, int), Cell> _grid;
}