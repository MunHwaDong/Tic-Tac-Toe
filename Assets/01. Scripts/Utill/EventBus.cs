using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    private static readonly IDictionary<GameEvent, UnityEvent> events = new Dictionary<GameEvent, UnityEvent>();

    public static void RegisterEvent(GameEvent eventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            events.Add(eventType, thisEvent);
            thisEvent.AddListener(action);
        }
    }

    public static void UnregisterEvent(GameEvent eventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(action);
        }
    }

    public static void Publish(GameEvent eventType)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}