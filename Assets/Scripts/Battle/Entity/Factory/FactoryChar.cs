using UnityEngine;
using System.Collections.Generic;

public class FactoryChar : AbstractFactory
{
    [SerializeField] private CharRank rank;    

    protected Dictionary<int, PooledEntity> pooledEntityDict;
    protected List<int> availableCodeList;

    public CharRank Rank => rank;
    public Dictionary<int, PooledEntity> PooledEntityDict => pooledEntityDict;

    public override void Initialize(Dictionary<int, Entity> entityDict)
    {
        pooledEntityDict = new Dictionary<int, PooledEntity>();
        availableCodeList = new List<int>();

        PooledEntity pooledEntity;
        GameObject[] gameObjects;
        GameObject go;
        int poolNum;

        foreach (var item in entityDict)
        {
            poolNum = (item.Value.Data as CharacterSO).poolNum;
            gameObjects = new GameObject[poolNum];

            for (int i = 0; i < poolNum; i++)
            {
                go = Instantiate(item.Value.gameObject);
                go.SetActive(false);
                gameObjects[i] = go;
            }

            pooledEntity = new PooledEntity(gameObjects, (item.Value.Data as CharacterSO).poolNum);
            pooledEntityDict.Add(item.Key, pooledEntity);
            availableCodeList.Add(item.Key);
        }
    }

    public override void ActiveEntity(SummonData data)
    {
        Vector3 position = data.Position;
        pooledEntityDict[data.CharCode].ActiveEntity(data);
    }

    public override void DeactiveEntity(int code)
    {
        pooledEntityDict[code].DeactiveEntity();

        if (availableCodeList.Contains(code) == false)
        {
            availableCodeList.Add(code);
        }
    }
}
