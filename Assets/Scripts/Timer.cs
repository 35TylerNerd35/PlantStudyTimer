using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEvent;

public class Timer : MonoBehaviour
{
    [SerializeField] int timerLength;
    int currentTime;

    void Start()
    {
        StartTimer();

        OnEndTimer.GetEvent<int>().AddListener(OnEndTimerMessageReceived);
    }

    void StartTimer()
    {
        // Setup timer
        currentTime = timerLength;
        OnStartTimer.InvokeEvent<int>(timerLength);

        // Start timer
        StartCoroutine(CountSecond());
    }

    void EndTimer()
    {
        Debug.Log($"Time: {currentTime}");
        OnEndTimer.InvokeEvent<int>(timerLength);
    }

    void OnEndTimerMessageReceived(int length)
    {
        Debug.Log("NEW TIMER");
        timerLength += 5;
        StartTimer();
    }

    IEnumerator CountSecond()
    {
        // Wait a second and take second away from count
        Debug.Log($"Time: {currentTime}");
        yield return new WaitForSeconds(1);
        currentTime -= 1;

        if (currentTime <= 0)
        {
            EndTimer();
        }
        else
        {
            // Repeat timer
            StartCoroutine(CountSecond());
        }

    }

}
