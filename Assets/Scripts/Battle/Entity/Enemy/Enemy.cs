public class Enemy : Entity, IDamagable
{
    private bool isDied;
    public bool IsDied => isDied;

    public override void EntityActivated()
    {
        //Debug.Log($"{gameObject.name} activated");
        if(battleData == null)
        {
            battleData = new BattleEnemyData(Data, this);
        }
        else
        {
            battleData.UpdateData(Data, this);
        }

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
