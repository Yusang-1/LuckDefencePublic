using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float hitRange;
    protected bool haveTarget;
    protected Entity target;
    protected ISkill[] skills;
    protected float speed;
    
    protected Vector3 direction;
    
    protected virtual void Update()
    {
        if(haveTarget == false) return;
        
        //타겟으로 이동
        direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * Time.deltaTime * speed;
        
        //타겟과의 거리 확인
        if(Vector3.Distance(transform.position, target.transform.position) <= hitRange)
        {
            HitTarget(target);
        }
    }
    
    protected virtual void HitTarget(Entity target)
    {
        foreach(var skill in skills)
        {
            skill.UseSkill(target);
        }        
            
        ResetProjectile();
        gameObject.SetActive(false);
    }
    
    public void SetTarget(Entity target, ISkill[] skills, float speed)
    {
        this.target = target;
        this.skills = skills;
        this.haveTarget = true;
        this.speed = speed;
    }
    
    protected void ResetProjectile()
    {
        this.target = null;
        this.skills = null;
        this.haveTarget = false;
        this.speed = 0f;
    }
}
