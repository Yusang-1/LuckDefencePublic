using UnityEngine;
using System;

[Serializable]
public class HitSingleTarget : ISkill
{
    Entity subject;
    
    [SerializeField] private float damageValue;
    
    public HitSingleTarget() { }
    
    public void UseSkill(Entity subject, Entity target)
    {
        if(target is IDamagable)
        {
            float damage = (subject.BattleData as BattleCharacterData).AttackPoint * damageValue;
            (target as IDamagable).TakeDamage((int)damage);            
        }
    }
}
