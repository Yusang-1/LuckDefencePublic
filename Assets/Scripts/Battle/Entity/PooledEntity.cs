using UnityEngine;
using System;

public class PooledEntity
{
    public event Action<int, GameObject> CharacterSpawned;

    private GameObject[] pooledEntities;
    private int nowPooledCount;
    private int maxPooledCount;    

    public PooledEntity(GameObject[] gameObjects, int poolNum)
    {
        pooledEntities = gameObjects;
        maxPooledCount = poolNum;
    }

    public GameObject GetLastActivatedEntity()
    {
        return pooledEntities[nowPooledCount - 1];
    }

    public bool ActiveEntity(SummonData data)
    {
        GameObject go = pooledEntities[nowPooledCount].gameObject;
        go.SetActive(true);
        pooledEntities[nowPooledCount].transform.position = data.Position;
        nowPooledCount++;

        CharacterSpawned?.Invoke(data.PlatformIndex, go);

        if(nowPooledCount == maxPooledCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeactiveEntity()
    {

    }

    public void DeactiveEntityAll()
    {
        foreach(GameObject go in pooledEntities)
        {
            go.SetActive(false);
        }
    }
}
