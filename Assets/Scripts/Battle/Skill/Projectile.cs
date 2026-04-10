using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public event Action<Entity> projectileHit;
    private BattleSkillData battleSkillData;
    
    [SerializeField] private float speed;
    [SerializeField] protected float hitRange;
    
    protected bool haveTarget;
    protected Entity subject;
    protected Entity target;
    protected Vector3 targetPosition => target.transform.position;
    
    private void Update()
    {
        if(haveTarget == false) return;
        
        MoveProjectile();
    }        
    
    private void MoveProjectile()
    {
        Vector3 directionVector = (targetPosition - transform.position).normalized;
        
        transform.position += directionVector * speed * Time.deltaTime;
        if(Vector3.Distance(transform.position, targetPosition) <= hitRange)
        {
            projectileHit?.Invoke(target);
            ResetProjectile();
        }
    }    
    
    public void SetProjectile(Entity subject, Entity target)
    {
        this.subject = subject;
        this.target = target;
        haveTarget = true;
        
        transform.position = subject.transform.position;
    }
    
    public virtual void SetProjectile(Entity subject, Entity target, BattleSkillData skillData)
    {
        this.subject = subject;
        this.target = target;
        haveTarget = true;
        
        battleSkillData = skillData;
        projectileHit += battleSkillData.UseSkillList;
        
        transform.position = subject.transform.position;
    }
    
    public virtual void ResetProjectile()
    {
        subject = null;
        target = null;
        haveTarget = false;
        
        projectileHit -= battleSkillData.UseSkillList;
        
        gameObject.SetActive(false);
    }
}
