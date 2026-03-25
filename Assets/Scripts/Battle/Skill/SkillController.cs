using System.Collections.Generic;
using UnityEngine;

public class SkillController
{
    private ISkill[] basicAttack;
    private ISkill[] skills;
    
    public void SetSkill(Entity subject)
    {
        CharacterSO data = subject.Data as CharacterSO;
        ISkill tempSkill;
        basicAttack = new ISkill[data.BasicAttack.EffectDatas.Length];
        
        int count = 0;
        foreach(var effectData in data.BasicAttack.EffectDatas)
        {
            tempSkill = effectData.CreateSkill(subject);
            basicAttack[count] = tempSkill;
            count++;
            
            if(tempSkill is ShootProjectile)
            {
                (tempSkill as ShootProjectile).SetPool(new ProjectilePool((ShootProjectileSkillEffect)effectData));
            }
        }
        
        skills = new ISkill[data.Skill.EffectDatas.Length];
        
        count = 0;
        foreach(var effectData in data.Skill.EffectDatas)
        {
            tempSkill = effectData.CreateSkill(subject);
            skills[count] = tempSkill;
            count++;
            
            if(tempSkill is ShootProjectile)
            {
                (tempSkill as ShootProjectile).SetPool(new ProjectilePool((ShootProjectileSkillEffect)effectData));
            }
        }
    }
    
    public void UseSkill(Entity target)
    {
        if (skills.Length == 0)
        {
            Debug.LogWarning("스킬이 설정되지 않았습니다.");
            return;
        }
        
        foreach(var skill in skills)
        {
            skill.UseSkill(target);
        }
    }
    
    public void UseBasicAttack(Entity target)
    {
        if (basicAttack.Length == 0)
        {
            Debug.LogWarning("기본 공격이 설정되지 않았습니다.");
            return;
        }
        
        foreach(var skill in basicAttack)
        {
            skill.UseSkill(target);        
        }
    }
}
