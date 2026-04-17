using UnityEngine;
using System;

public class BattleTimer : AbstractTimer
{
    public event Action TimeIsOver;
    public event Action<float> TimeChanged;

    private float minTimeUnit = 0.01f;
    //private float warningTime = 10;    

    private float elapsedTime;
    private int timeCount;
    protected float currentTime;
    protected float CurrentTime
    {
        get => currentTime;
        set
        {
            if(currentTime == value) return;
            
            currentTime = Mathf.Clamp(value, 0, value);
            TimeChanged?.Invoke(currentTime);

            if (currentTime == 0 && isTimerSet)
            {
                TimeIsOver?.Invoke();
            }
        }
    }    

    public override void TimerUpdate()
    {
        if (CurrentTime == 0 || isTimerSet == false)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= minTimeUnit)
        {
            timeCount = 0;
            while (elapsedTime > minTimeUnit)
            {
                elapsedTime -= minTimeUnit;
                timeCount++;
            }

            CurrentTime -= Mathf.Clamp(minTimeUnit * timeCount, 0, minTimeUnit * timeCount);
        }
    }
    
    public BattleTimer()
    {
        TimerManager.AddTimer(this);
        
        
    }

    public void Initialize()
    {
        CurrentTime = 0;
    }

    public void OnStartTimerAddTime(RoundData data)
    {
        isTimerSet = true;

        CurrentTime += data.AdditionalTime;
    }

    public override void ResetTimer()
    {
        isTimerSet = false;

        CurrentTime = 0;
        
        TimerManager.RemoveTimer(this);
    }
    
    public void OnResetTimer()
    {
        ResetTimer();
    }
}
