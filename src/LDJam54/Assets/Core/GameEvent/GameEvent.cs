using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventSubscription> listeners = new List<GameEventSubscription>();

    public void Publish()
    {
        CleansedListeners.ToList().ForEach(l => l.OnEvent(l));
    }

    public void Subscribe(Action action, object subscriber) => Subscribe(new GameEventSubscription(name, x => action(), subscriber));
    public void Subscribe(GameEventListener listener) => Subscribe(new GameEventSubscription(name, x => listener.OnEventRaised(), listener));
    public void Unsubscribe(GameEventListener listener) => Unsubscribe((object) listener);

    public void Subscribe(GameEventSubscription e)
    {
        listeners.Add(e);
    }

    public void Unsubscribe(object owner)
    {
        listeners.RemoveAll(l => l.Owner == owner);
    }

    private IEnumerable<GameEventSubscription> CleansedListeners => listeners.Where(x => x.Owner != null);
}
