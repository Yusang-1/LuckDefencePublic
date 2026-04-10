using UnityEngine;

public class BattleEnemyData : BattleEntityData
{
    public BattleEnemyData(EntitySO data, Entity entity)
    {
        this.data = data;
        this.entity = entity;        
        MoveSpeed = data.MoveSpeed;
        CurrentHP = data.MaxHp;        
        defencePoint = data.defaultDefencePoint;            
    }
}
