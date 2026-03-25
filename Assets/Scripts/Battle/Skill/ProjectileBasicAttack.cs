using UnityEngine;

public class ProjectileBasicAttack : Projectile
{
    private Entity subject;
    
    public void SetTarget(Entity target, ISkill[] skills, float speed, Entity subject)
    {
        base.SetTarget(target, skills, speed);
        this.subject = subject;
    }
    
    protected override void HitTarget(Entity target)
    {
        if(target != null)
        {
            Character character = subject as Character;
            character.DealDamage(target as Enemy);
        }
        else
        {
            Debug.Log("Target is null");
        }
        
        ResetProjectile();
        gameObject.SetActive(false);
    }
}
