using UnityEngine;
using System;

[Serializable]
public class BuffSlow : IBuff
{
    private BuffTimer timer;
    
    private float reducedAmount;
    [SerializeField] private float slowValue;
    [SerializeField] private float duration;
    
    public BuffSlow() { }
    
    /// <summary>
    /// 버프 적용, 시간이 duration만큼 지나면 자동적으로 해제 duration이 0인 경우 RemoveBuff를 호출해 해제해야함
    /// </summary>
    /// <param name="target"></param>
    public void UseSkill(Entity subject, Entity target)
    {
        ApplyBuff(target);
        
        if(duration == 0) return;
        
        if(timer == null)
        {
            timer = new BuffTimer();
        }
        timer.TimerOn += RemoveBuff;
        timer.SetTimer(duration, target);
    }
    
    public void ApplyBuff(Entity target)
    {
        float resultSpeed = target.BattleData.MoveSpeed * slowValue;
        reducedAmount = target.BattleData.MoveSpeed - resultSpeed;
        target.BattleData.MoveSpeed = resultSpeed;
    }
    
    /// <summary>
    /// duration에 상관없이 버프 해제, 타이머 리셋
    /// </summary>
    /// <param name="target"></param>
    public void RemoveBuff(Entity target)
    {
        target.BattleData.MoveSpeed += reducedAmount;
        
        if(duration == 0) return;
        
        timer.TimerOn -= RemoveBuff;
        timer.ResetTimer();
    }
}
