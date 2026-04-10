using UnityEngine;
using System;

public class BattleEnemyBossData : BattleEntityData, ISkillUsableData
{
    public event Action<int> MPChanged;
    
    public BattleEnemyBossData(EntitySO data, Entity entity)
    {
        this.data = data;
        this.entity = entity;
        GetMPPoint = data.GetMPPoint;        
        MoveSpeed = data.MoveSpeed;
        CurrentHP = data.MaxHp;
        CurrentMP = 0;
        defencePoint = data.defaultDefencePoint;
        
        EnemyBossSO bossSO = data as EnemyBossSO;        
        SkillData = new BattleSkillData(entity, bossSO.SkillData.CreateProjectilePool(), bossSO.SkillData);
    }
    
    public int MaxMp => data.MaxMp;
    private int currentMP;
    public int CurrentMP
    {
        get => currentMP;
        set
        {
            if(entity is ISkillusable == false) return;
            
            currentMP = Mathf.Clamp(value, 0, data.MaxMp);
            MPChanged?.Invoke(currentMP);

            if(currentMP >= data.MaxMp)
            {
                //(entity as ISkillusable).UseSkill();
            }
        }
    }
    public int GetMPPoint;
    
    public BattleSkillData SkillData;
}
