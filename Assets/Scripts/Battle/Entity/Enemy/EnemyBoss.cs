using UnityEngine;

public class EnemyBoss : Enemy, ISkillusable
{
    public override void TakeDamage(int damage)
    {
        BattleData.CurrentHP -= damage;

        GetMP(Data.GetMPPoint);
    }
    
    public void GetMP(int amount)
    {
        BattleData.CurrentMP += amount;
    }    

    public void UseSkill(IDamagable target)
    {
        BattleData.CurrentMP = 0;
    }
}
