public class HitSingleTarget : ISkill
{
    private SkillEffectSO data;
    private Entity subject;

    public HitSingleTarget(SkillEffectSO data, Entity subject)
    {
        this.data = data;
        this.subject = subject;
    }
    
    public void UseSkill(Entity target)
    {
        int damage = (int)(data.Value * (subject.BattleData.AttackPoint - target.BattleData.defencePoint));
        (target as IDamagable).TakeDamage(damage);
    }
}
