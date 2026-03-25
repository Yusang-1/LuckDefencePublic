public class Silence : IBuff
{
    private SkillEffectSO data;  

    public Silence(SkillEffectSO data)
    {
        this.data = data;
    }
    
    public void UseSkill(Entity target)
    {
        target.BuffController.GetBuff(ApplyBuff, ReleaseBuff);
    }
    
    public float ApplyBuff(Entity target)
    {
        target.BattleData.GetMPPoint = 0;

        return (data as MaintainSkillEffectSO).EffectLength;
    }

    public void ReleaseBuff(Entity target)
    {
        target.BattleData.GetMPPoint = target.Data.GetMPPoint;
    }
}
