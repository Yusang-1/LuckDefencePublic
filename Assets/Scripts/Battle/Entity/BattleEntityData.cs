using UnityEngine;
using System;

public class BattleEntityData
{
    public event Action<int> HPChanged;
    
    public event Action EntityDied;
    
    protected Entity entity;
    protected EntitySO data;
    
    // public BattleEntityData(EntitySO data, Entity entity)
    // {
    //     this.data = data;
    //     this.entity = entity;
    //     GetMPPoint = data.GetMPPoint;
    //     AttackPoint = data.AttackPoint;
    //     AttackSpeed = data.AttackSpeed;
    //     AttackRange = data.AttackRange;
    //     MoveSpeed = data.MoveSpeed;
    //     CurrentHP = data.MaxHp;
    //     CurrentMP = 0;
    //     defencePoint = data.defaultDefencePoint;
        
    //     //임시
    //     Attack = data.AttackData.CreateSkill();
    //     AttackPool = new ProjectilePool(entity, data.AttackData, (data.AttackData.SkillEffect as EffectShootProjectile).HitEffect);
    //     Skill = data.SkillData.CreateSkill();
    //     AttackPool = new ProjectilePool(entity, data.SkillData, (data.SkillData.SkillEffect as EffectShootProjectile).HitEffect);
    // }
    
    public void UpdateData(EntitySO newData, Entity entity)
    {
        this.data = newData;
        this.entity = entity;
        // GetMPPoint = data.GetMPPoint;
        // AttackPoint = data.AttackPoint;
        // AttackSpeed = data.AttackSpeed;
        // AttackRange = data.AttackRange;
        MoveSpeed = data.MoveSpeed;
        CurrentHP = data.MaxHp;
        // CurrentMP = 0;
        defencePoint = data.defaultDefencePoint;
    }
    
    private int currentHP;
    
    
    public int Code => data.Code;
    public string EntityName => data.EntityName;
    public int MaxHp => data.MaxHp;
    public int CurrentHP
    {
        get => currentHP;
        set
        {
            if(entity is IDamagable == false) return;
            
            currentHP = Mathf.Clamp(value, 0, data.MaxHp);
            HPChanged?.Invoke(currentHP);

            if(currentHP <= 0)
            {
                (entity as IDamagable).Die();
                EntityDied?.Invoke();
            }
        }
    }
    
    public float defencePoint;
    public float MoveSpeed;    
}
