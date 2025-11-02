using System.Collections;
using TMPro;
using UnityEngine;
using static GameEvent;

public enum TimerType
{
    Pomodoro,
    ShortBreak,
    LongBreak
}

public class Timer : MonoBehaviour
{
    int currentTime;
    int pomodoroAmount;
    TimerType currentTimerType;
    bool isLongBreak15;

    [SerializeField] TMP_Text timerText;
    [SerializeField] AudioClip timerSound;

    void Awake()
    {
        OnEndTimer.GetEvent<TimerType>().AddListener(OnEndTimerMessageReceived);
    }

    public void BeginPomodoro()
    {
        StartTimer(TimerType.Pomodoro);
    }

    void StartTimer(TimerType type)
    {
        // Select appropriate time amount
        switch (type)
        {
            case TimerType.Pomodoro:
                currentTime = 25;
                break;
            case TimerType.ShortBreak:
                currentTime = 5;
                break;
            case TimerType.LongBreak:
                currentTime = isLongBreak15 ? 15 : 30;
                break;
        }

        // Setup timer type
        currentTimerType = type;

        // Start timer
        OnStartTimer.InvokeEvent(type);
        StartCoroutine(CountSecond());
    }

    void OnEndTimerMessageReceived(TimerType timerType)
    {
        // Alert user
        GetComponent<AudioSource>().PlayOneShot(timerSound);

        // Deal with finishing Pomodoro
        if (timerType == TimerType.Pomodoro)
        {
            pomodoroAmount += 1;

            if (pomodoroAmount % 4 == 0)
            {
                StartTimer(TimerType.LongBreak);
            }
            else
            {
                StartTimer(TimerType.ShortBreak);
            }
            return;
        }

        StartTimer(TimerType.Pomodoro);
    }

    IEnumerator CountSecond()
    {
        // Wait a second and take second away from count
        timerText.text = currentTime.ToString();
        yield return new WaitForSeconds(1);
        currentTime -= 1;

        if (currentTime <= 0)
        {
            OnEndTimer.InvokeEvent(currentTimerType);
        }
        else
        {
            // Repeat timer
            StartCoroutine(CountSecond());
        }

    }

}
