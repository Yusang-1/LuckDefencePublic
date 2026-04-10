using UnityEngine;
using System;

public class IntervalTimer : AbstractTimer
{
    public event Action<Entity> IntervalOn;
    public event Action<Entity> TimerOver;        
    
    private Entity target;
    float intervalTime;
    float intervalCount;
    float destTime;
    float elapsedTime;
    
    public override void TimerUpdate()
    {
        if(isTimerSet == false) return;
        
        elapsedTime += Time.deltaTime;
        
        if(elapsedTime >= intervalTime * intervalCount)
        {
            IntervalOn?.Invoke(target);
            intervalCount++;
        }
        
        if(elapsedTime >= destTime)
        {
            TimerOver?.Invoke(target);
            ResetTimer();
        }
    }
    
    public void SetTimer(float destTime, float intervalTime, Entity target)
    {
        this.intervalTime = intervalTime;
        this.destTime = destTime;
        elapsedTime = 0;
        intervalCount = 0;
        this.target = target;
        
        isTimerSet = true;
    }
    
    public override void ResetTimer()
    {
        isTimerSet = false;
        TimerManager.RemoveTimer(this);
    }
}
