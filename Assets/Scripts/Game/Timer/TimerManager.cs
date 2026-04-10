using UnityEngine;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{
    private static List<AbstractTimer> timers;

    private void Start()
    {
        if(timers == null)
        {
            timers = new List<AbstractTimer>();
        }
    }
    
    private void Update()
    {
        if(timers.Count == 0)
        {
            return;
        }
        
        foreach(var timer in timers)
        {
            timer.TimerUpdate();
        }
    }
    
    public static void AddTimer(AbstractTimer newTimer)
    {
        if(timers == null)
        {
            timers = new List<AbstractTimer>();
        }
        
        timers.Add(newTimer);
    }
    
    public static void RemoveTimer(AbstractTimer timer)
    {
        if(timers == null) return;
        
        timers.Remove(timer);
    }
}
