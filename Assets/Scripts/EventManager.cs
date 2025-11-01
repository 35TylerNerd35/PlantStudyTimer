using System.Collections.Generic;
using UnityEngine.Events;

// Define events
public enum GameEvent
{
    OnStartTimer,
    OnEndTimer
}

public static class EventManager
{
    // Establish a dictionary of events to search
    public static Dictionary<GameEvent, object> events = new();

    // Access and find event
    public static UnityEvent<T> GetEvent<T>(this GameEvent e)
    {
        TryAccessEvent<T>(e);
        return (UnityEvent<T>)events[e];
    }

    // Find and invoke event
    public static void InvokeEvent<T>(this GameEvent e, T data)
    {
        GetEvent<T>(e).Invoke(data);
    }

    private static void TryAccessEvent<T>(GameEvent e)
    {
        // Do nothing if event exists
        if (events.ContainsKey(e))
            return;

        // Create new event and add to dictionary
        events.Add(e, new UnityEvent<T>());
    }
}

/*
using static GameEvent;

{EventName}.InvokeEvent<{EventType}>({Data});
{EventName}.GetEvent<{EventType}>();


*/