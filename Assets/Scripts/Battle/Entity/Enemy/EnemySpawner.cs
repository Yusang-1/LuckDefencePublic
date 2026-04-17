using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private HPSpawner hpSpawner;

    [SerializeField] private BattleDataSO battleData;

    private IEnumerator activeEnemyCoroutine;

    private Dictionary<RoundData, Entity[]> entitiesByRoundData;

    public void Initialize(RoundData[] roundDatas, HPSpawner hpSpawner, Transform spawnArea)
    {
        this.hpSpawner = hpSpawner;

        GameObject go;
        Entity[] entities;
        entitiesByRoundData = new Dictionary<RoundData, Entity[]>();

        foreach (RoundData roundData in roundDatas)
        {
            entities = new Entity[roundData.EnemyCount];

            for (int i = 0; i < roundData.EnemyCount; i++)
            {
                go = Instantiate(roundData.Enemy.gameObject, spawnArea.position, Quaternion.identity);
                go.SetActive(false);
                entities[i] = go.GetComponent<Entity>();
            }

            entitiesByRoundData.Add(roundData, entities);
        }
    }

    public void ResetSpawner()
    {
        if(activeEnemyCoroutine != null)
        {
            StopCoroutine(activeEnemyCoroutine);
        }
    }

    public void SpawnEnemy(RoundData roundData)
    {       
        activeEnemyCoroutine = ActiveEnemyCoroutine(entitiesByRoundData[roundData], roundData.SpawnDelay);
        StartCoroutine(activeEnemyCoroutine);
    }

    private IEnumerator ActiveEnemyCoroutine(Entity[] entities, float spawnDelay)
    {
        WaitForSeconds waitSpawnDelay = new WaitForSeconds(spawnDelay);
        for(int i = 0; i < entities.Length; i++)
        {
            entities[i].gameObject.SetActive(true);
            entities[i].EntityActivated();

            entities[i].Mover.Initialize(entities[i]);

            battleData.CurrentEnemyCount++;

            hpSpawner.ActivateHP(entities[i]);

            yield return waitSpawnDelay;
        }
    }

    public void OnStopActiveCoroutine()
    {
        if(activeEnemyCoroutine != null)
        {
            StopCoroutine(activeEnemyCoroutine);
        }
    }
}
