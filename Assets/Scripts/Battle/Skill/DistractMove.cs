public class DistractMove : IBuff
{
    private SkillEffectSO data;  

    public DistractMove(SkillEffectSO data)
    {
        this.data = data;
    }
    
    public void UseSkill(Entity target)
    {
        target.BuffController.GetBuff(ApplyBuff, ReleaseBuff);
    }
    
    public float ApplyBuff(Entity target)
    {
        if(data.Value == 0)
        {
            target.BattleData.MoveSpeed = 0;
        }
        else
        {
            target.BattleData.MoveSpeed -= data.Value;
        }        

        return (data as MaintainSkillEffectSO).EffectLength;
    }

    public void ReleaseBuff(Entity target)
    {
        if(data.Value == 0)
        {
            target.BattleData.MoveSpeed = target.Data.MoveSpeed;
        }
        else
        {
            target.BattleData.MoveSpeed += data.Value;            
        }
    }
}
