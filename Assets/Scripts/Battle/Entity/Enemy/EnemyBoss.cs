using UnityEngine;

public class EnemyBoss : Enemy, ISkillusable
{
    private BattleEnemyBossData battleEnemyBossData => battleData as BattleEnemyBossData;
    
    public override void TakeDamage(int damage)
    {
        BattleData.CurrentHP -= damage;

        GetMP(Data.GetMPPoint);
    }
    
    public void GetMP()
    {
        battleEnemyBossData.CurrentMP += battleEnemyBossData.GetMPPoint;
    } 
    
    public void GetMP(int amount)
    {
        battleEnemyBossData.CurrentMP += amount;
    }

    public void UseSkill(IDamagable target)
    {
        battleEnemyBossData.CurrentMP = 0;
    }
}
