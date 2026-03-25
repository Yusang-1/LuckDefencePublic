using System.Collections.Generic;
using UnityEngine;

public class HitMultipleTarget : ISkill
{
    private Entity subject;
    private SkillEffectSO data;
    
    public HitMultipleTarget(SkillEffectSO data, Entity subject)
    {
        this.subject = subject;
        this.data = data;
    }
    
    public void UseSkill(Entity target)
    {
        List<Entity> targets = GetTargetsInRange(target);
        
        foreach(var entity in targets)
        {
            int damage = (int)(data.Value * (subject.BattleData.AttackPoint - entity.BattleData.defencePoint));
            entity.BattleData.CurrentHP -= damage;
        }
    }
    
    private List<Entity> GetTargetsInRange(Entity target)
    {
        List<Entity> targets = new List<Entity>();
        MultipleTargetSkillEffect multiSkillData = data as MultipleTargetSkillEffect;
        
        foreach(var entity in EnemyList.Enemies)
        {
            if(Vector3.Distance(target.transform.position, entity.gameObject.transform.position) <= multiSkillData.HitRange)
            {
                targets.Add(entity);
            }
        }
        
        return targets;
    }
}
