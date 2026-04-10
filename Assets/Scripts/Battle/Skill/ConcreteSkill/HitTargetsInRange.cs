using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class HitTargetsInRange : ISkill
{
    [SerializeField] private float damageValue;
    [SerializeField] private float range;
    
    public HitTargetsInRange() { }
    
    public void UseSkill(Entity subject, Entity target)
    {
        List<Entity> targets = SearchTargetsInRange(target.transform.position);
        
        foreach(Entity entity in targets)
        {
            if((entity as Enemy).IsDied) continue;
            
            (entity as IDamagable).TakeDamage((int)(damageValue * (subject.BattleData as BattleCharacterData).AttackPoint));
        }
    }
    
    private List<Entity> SearchTargetsInRange(Vector3 standardPosition)
    {
        List<Entity> entities = EnemyList.Enemies.ToList<Entity>();
        List<Entity> targets = new List<Entity>();
        
        foreach(Entity entity in entities)
        {
            if(Vector3.Distance(standardPosition, entity.transform.position) <= range)
            {
                if((entity as Enemy).IsDied) continue;
                
                targets.Add(entity);
            }
        }
        
        return targets;
    }
}
