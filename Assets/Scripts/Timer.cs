using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer //: MonoBehaviour
{
    private float currentTime;
    private float maxTime;
    private bool timerCompleted;

    public float CurrentTime { get => currentTime; }
    public float MaxTime { get => maxTime; }
    public bool TimerCompleted { get => timerCompleted;  }

    public Timer(float endTime)
    {
        currentTime = 0;
        maxTime = endTime;
        timerCompleted = false;
    }

    //Updates the timer and checks if it is completed.
    //Use Time.DeltaTime for elapsedTime to use UpdateTimer in frame update.
    //Use custom values when not keeping track of the timer in Update()
    public void UpdateTimer(float elapsedTime)
    {
        currentTime += elapsedTime;
        if(currentTime >= maxTime)
        {
            timerCompleted = true;
        }
    }

    public void ResetTimer()
    {
        currentTime = 0;
        timerCompleted = false;
    }

    public void ResetTimer(float newEndTime)
    {
        currentTime = 0;
        timerCompleted = false;
        maxTime = newEndTime;
    }
}
