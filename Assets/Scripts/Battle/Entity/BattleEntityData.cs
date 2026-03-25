using UnityEngine;
using System;

public class BattleEntityData
{
    public event Action<int> HPChanged;
    public event Action<int> MPChanged;
    public event Action EntityDied;
    
    private Entity entity;
    private EntitySO data;
    
    public BattleEntityData(EntitySO data, Entity entity)
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
    }
    
    public void UpdateData(EntitySO newData, Entity entity)
    {
        this.data = newData;
        this.entity = entity;
        GetMPPoint = data.GetMPPoint;
        AttackPoint = data.AttackPoint;
        AttackSpeed = data.AttackSpeed;
        AttackRange = data.AttackRange;
        MoveSpeed = data.MoveSpeed;
        CurrentHP = data.MaxHp;
        CurrentMP = 0;
        defencePoint = data.defaultDefencePoint;
    }
    
    private int currentHP;
    private int currentMP;
    
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
    public int MaxMp => data.MaxMp;
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
    public int AttackPoint;
    public float AttackSpeed;
    public float AttackRange;
    public float defencePoint;
    public float MoveSpeed;    
}
