using System;
using UnityEngine;
using static GameEvent;

public class PomodoroManager : MonoBehaviour
{
    TimerType timerType;
    public static Day currentDay = new();
    public static Pomodoro currentPomodoro = new();

    void Awake()
    {
        OnStartTimer.GetEvent<TimerType>().AddListener((val) => timerType = val);
        OnEndTimer.GetEvent<TimerType>().AddListener(OnEndTimerMessage);
    }

    private void OnEndTimerMessage(TimerType arg0)
    {
        if (arg0 != TimerType.Pomodoro)
            return;

        currentPomodoro.wasCompleted = true;

        // Submit pomodoro to day
        currentDay.pomodoros.Add(currentPomodoro);
        currentPomodoro = new();
        Debug.Log(JsonUtility.ToJson(currentDay));
    }

    public void TimerInterrupted()
    {
        if (timerType != TimerType.Pomodoro)
            return;

        OnPomodoroInterrupted.InvokeEvent<string>(null);
        currentPomodoro.wasInterrupted = true;
    }
}
