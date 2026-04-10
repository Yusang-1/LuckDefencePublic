using UnityEngine;
using System;

[Serializable]
public class HitBasicAttack : ISkill
{
    [SerializeField] private float damageValue;
    
    public HitBasicAttack() { }
    
    public void UseSkill(Entity subject, Entity target)
    {
        if(target is IDamagable)
        {
            float damage = (subject.BattleData as BattleCharacterData).AttackPoint * damageValue;
            (target as IDamagable).TakeDamage((int)damage);
            (subject as Character).GetMP();
        }
    }
}
