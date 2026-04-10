using UnityEngine;
using System;
using System.Collections.Generic;

public class ProjectileArea : Projectile
{
    public event Action<Entity> InProjectile;    
    public event Action<Entity> OutProjectile;
    private BattleSkillData battleSkillData;
    
    private float durationTime = 2.4f;
    private float elapsedTime;
    
    private void Update()
    {
        if(haveTarget == false) return;
        
        CheckEntityInArea();
        CheckDuration();        
    }
    
    public void Initialize(int effectsCount)
    {
        transform.position = target.transform.position;
        entitiesInArea = new HashSet<Entity>();
        //base.Initialize(effectsCount);
    }
    
    public override void SetProjectile(Entity subject, Entity target, BattleSkillData skillData)
    {
        this.subject = subject;
        this.target = target;
        haveTarget = true;
        
        battleSkillData = skillData;

        InProjectile += skillData.UseSkillList;
        OutProjectile += skillData.CancelSkillList;
        
        transform.position = target.transform.position;
    }
    
    HashSet<Entity> entitiesInArea;
    private void CheckEntityInArea()
    {
        if(entitiesInArea == null)
        {
            entitiesInArea = new HashSet<Entity>();
        }
        
        List<Entity> entities = EnemyList.Enemies;
        foreach(var entity in entities)
        {
            if(Vector3.Distance(transform.position, entity.transform.position) <= hitRange)
            {
                if(entitiesInArea.Contains(entity) == false)
                {
                    InProjectile?.Invoke(entity);
                    
                    entitiesInArea.Add(entity);
                }
            }
            else
            {
                if(entitiesInArea.Contains(entity) == true)
                {
                    OutProjectile?.Invoke(entity);
                    entitiesInArea.Remove(entity);
                }
            }
        }        
    }
    
    /// <summary>
    /// projectile의 지속시간 측정
    /// </summary>
    private void CheckDuration()
    {
        elapsedTime += Time.deltaTime;
        
        if(elapsedTime >= durationTime)
        {
            ResetProjectile();
        }
    }

    public override void ResetProjectile()
    {
        foreach(var entity in entitiesInArea)
        {
            OutProjectile?.Invoke(entity);
        }
        entitiesInArea.Clear();
        
        elapsedTime = 0;
        
        InProjectile -= battleSkillData.UseSkillList;
        OutProjectile -= battleSkillData.CancelSkillList;
        
        subject = null;
        target = null;
        haveTarget = false;
        
        gameObject.SetActive(false);
    }
}
