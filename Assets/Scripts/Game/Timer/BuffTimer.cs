using UnityEngine;
using System;

public class BuffTimer : AbstractTimer
{    
    public event Action<Entity> TimerOn;
    
    private Entity target;
    private float destTime;
    private float elapsedTime;
    
    public BuffTimer()
    {
        TimerManager.AddTimer(this);
    }
    
    public override void TimerUpdate()
    {
        if(isTimerSet == false) return;
        
        elapsedTime += Time.deltaTime;
        
        if(elapsedTime >= destTime)
        {
            TimerOn?.Invoke(target);
            ResetTimer();
        }
    }
    
    public void SetTimer(float destTime, Entity target)
    {
        elapsedTime = 0;
        this.destTime = destTime;
        this.target = target;
        this.target = target;
        
        isTimerSet = true;
    }
    
    public override void ResetTimer()
    {
        isTimerSet = false;
        TimerManager.RemoveTimer(this);
    }
}
