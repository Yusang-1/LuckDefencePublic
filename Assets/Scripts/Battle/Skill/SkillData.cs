using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SkillData
{
    [SerializeField] string skillName;
    [SerializeField] string skillDescription;
    [SerializeField] GameObject projectile;
    [SerializeField] int poolCount;
    [SerializeField] GameObject hitVFX;
    
    [SerializeReference] public List<ISkill> SkillAbilities;
    
    public ProjectilePool CreateProjectilePool()
    {
        return new ProjectilePool(projectile, poolCount);
    }
}
