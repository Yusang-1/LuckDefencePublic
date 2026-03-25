using UnityEngine;
using System.Collections.Generic;

public class EnemyList : MonoBehaviour
{
    public static List<Entity> Enemies = new List<Entity>();

    [SerializeField] private BattleDataSO battleData;

    private static BattleDataSO s_BattleData;

    private void Start()
    {        
        s_BattleData = battleData;
        Enemies.Clear();
    }

    public static void Activated(Entity entity)
    {
        Enemies.Add(entity);
    }

    public static void Deactivated(Entity entity)
    {
        Enemies.Remove(entity);
        s_BattleData.CurrentEnemyCount--;
        s_BattleData.CurrentCoin += (entity.Data as EnemySO).DropCoin;
    }

    public void OnDeactivateAllEnemy()
    {
        foreach(var enemy in Enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        Enemies.Clear();
    }
}
