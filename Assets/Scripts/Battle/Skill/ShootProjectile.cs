using UnityEngine;

public class ShootProjectile : ISkillShootProjectile
{
    private SkillEffectSO data;
    private Entity subject;
    private ProjectilePool pool;
    
    public ShootProjectile(SkillEffectSO data, Entity subject)
    {
        this.data = data;
        this.subject = subject;
    }
    
    public void SetPool(ProjectilePool pool)
    {
        this.pool = pool;
    }
    
    public void UseSkill(Entity target)
    {
        ShootSkillPrefab(target);
    }

    public void ShootSkillPrefab(Entity target)
    {
        Projectile projectile = pool.GetProjectile();
        projectile.transform.position = subject.transform.position;
        
        ShootProjectileSkillEffect shootData = data as ShootProjectileSkillEffect;        
        ISkill[] skills = new ISkill[shootData.HitEffects.Length];
        for(int i = 0; i < shootData.HitEffects.Length; i++)
        {
            skills[i] = shootData.HitEffects[i].CreateSkill(subject);
        }
        
        if(projectile is ProjectileBasicAttack)
        {
            ProjectileBasicAttack basicProjectile = projectile as ProjectileBasicAttack;
            basicProjectile.SetTarget(target, skills, shootData.ProjectileSpeed, subject);
        }
        else
        {
            projectile.SetTarget(target, skills, shootData.ProjectileSpeed);
        }
        
        projectile.gameObject.SetActive(true);
    }
}
