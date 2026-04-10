using UnityEngine;

public abstract class AbstractTimer
{
    protected bool isTimerSet;
    
    public abstract void TimerUpdate();
    public abstract void ResetTimer();
}
