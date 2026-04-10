using UnityEngine;
using System;

public class BattleCharacterData : BattleEntityData, ISkillUsableData
{
    public event Action<int> MPChanged;
    
    public BattleCharacterData(EntitySO data, Entity entity)
    {
        this.data = data;
        this.entity = entity;
        GetMPPoint = data.GetMPPoint;
        AttackPoint = data.AttackPoint;
        AttackSpeed = data.AttackSpeed;
        AttackRange = data.AttackRange;
        MoveSpeed = data.MoveSpeed;
        CurrentHP = data.MaxHp;
        CurrentMP = 0;
        defencePoint = data.defaultDefencePoint;
        
        //임시
        CharacterSO characterData = data as CharacterSO;        
        
        AttackData = new BattleSkillData(entity, characterData.AttackData.CreateProjectilePool(), characterData.AttackData);
        SkillData = new BattleSkillData(entity, characterData.SkillData.CreateProjectilePool(), characterData.SkillData);
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

            // if(currentMP >= data.MaxMp)
            // {
            //     (entity as Character).IsManaFull = true;
            // }
        }
    }
    public int GetMPPoint;
    public int AttackPoint;
    public float AttackSpeed;
    public float AttackRange;    
    
    public BattleSkillData AttackData;
    public BattleSkillData SkillData;
}
