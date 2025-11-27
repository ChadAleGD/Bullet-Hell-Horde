using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventBus
{


    private static readonly Dictionary<EventType, List<Action<object>>> _eventSubscriptions = new();



    //------------------------------------------------------------------------------------------------//



    public static void Subscribe(EventType eventType, Action action) => Subscribe(eventType, _ => action());

    public static void Subscribe(EventType eventType, Action<object> action)
    {
        if (!_eventSubscriptions.ContainsKey(eventType)) _eventSubscriptions.Add(eventType, new());

        _eventSubscriptions[eventType].Add(action);
    }



    public static void Unsubscribe(EventType eventType, Action action) => Unsubscribe(eventType, _ => action());

    public static void Unsubscribe(EventType eventType, Action<object> action)
    {
        if (_eventSubscriptions.TryGetValue(eventType, out var actions) && actions.Contains(action))
            actions.Remove(action);
    }



    public static void Publish(EventType eventType, object data = null)
    {
        if (!_eventSubscriptions.ContainsKey(eventType)) return;
        foreach (var regEvent in _eventSubscriptions[eventType]) regEvent?.Invoke(data);
    }




}


public enum EventType
{
    OnRoundStart,
    OnRoundEnd,
    OnEnemyDied,
    OnEnterShop,
    OnExitShop,
    OnUpgradePanelOpen,
    OnUpgradePanelClose
}