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
        _player1PointText.text = gameData.player1WinCounter.ToString();
        _player2PointText.text = gameData.player2WinCounter.ToString();
    }
    
    [SerializeField] private TextMeshProUGUI _player1PointText;
    [SerializeField] private TextMeshProUGUI _player2PointText;
}
