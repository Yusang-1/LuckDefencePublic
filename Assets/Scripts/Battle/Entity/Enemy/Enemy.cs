public class Enemy : Entity, IDamagable
{
    private bool isDied;

    public override void EntityActivated()
    {
        base.EntityActivated();

        EnemyList.Activated(this);

        BattleData.CurrentHP = Data.MaxHp;

        isDied = false;
    }

    public virtual void TakeDamage(int damage)
    {
        BattleData.CurrentHP -= damage;        
    }

    public void Die()
    {
        if(isDied)
        {
            return;
        }
        
        isDied = true;
        EnemyList.Deactivated(this);
        gameObject.SetActive(false);
    }
}
