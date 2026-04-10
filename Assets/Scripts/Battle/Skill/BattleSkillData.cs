using System.Collections.Generic;

public class BattleSkillData
{
    protected Entity subject;
    protected ProjectilePool projectilePool;
    protected List<ISkill> skillList;
    
    public BattleSkillData(Entity subject, ProjectilePool pool, SkillData skillData)
    {
        this.subject = subject;
        projectilePool = pool;
        
        skillList = skillData.SkillAbilities;
    }
    
    /// <summary>
    /// Projectile클래스의 event에 할당
    /// </summary>
    /// <param name="target"></param>
    public void UseSkillList(Entity target)
    {
        foreach(ISkill skill in skillList)
        {
            skill.UseSkill(subject, target);
        }
    }
    
    /// <summary>
    /// ProjectileArea클래스의 event에 할당
    /// </summary>
    /// <param name="target"></param>
    public void CancelSkillList(Entity target)
    {
        foreach(ISkill skill in skillList)
        {
            if(skill is IBuff)
            {
                (skill as IBuff).RemoveBuff(target);
            }            
        }
    }
    
    public virtual void UseSkill(Entity target)
    {
        Projectile projectile = projectilePool.GetProjectile();
        projectile.SetProjectile(subject, target, this);
    }
}
