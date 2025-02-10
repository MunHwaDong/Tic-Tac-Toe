using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResultWindow : MonoBehaviour, IUIElement
{
    public void Init(UIController uiController)
    {
        EventBus.RegisterEvent(GameEvent.GAME_END, () => gameObject.SetActive(true));
        EventBus.RegisterEvent(GameEvent.GAME_START, () => gameObject.SetActive(false));
        
        uiController.updateUI += UpdateElement;
    }

    public void UpdateElement(GameData gameData)
    {
        
    }

    [SerializeField] private TextMeshProUGUI _resultText;
}
