using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPanels : MonoBehaviour, IUIElement
{
    public void Init(UIController uiController)
    {
        uiController.updateUI += UpdateElement;
    }

    public void UpdateElement(GameData gameData)
    {
        _player1PointText.text = gameData.player1WinPoint.ToString();
        _player2PointText.text = gameData.player2WinPoint.ToString();
    }
    
    [SerializeField] private TextMeshProUGUI _player1PointText;
    [SerializeField] private TextMeshProUGUI _player2PointText;
}
