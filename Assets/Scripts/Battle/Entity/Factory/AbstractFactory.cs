using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected int pooledNum;

    public virtual void Initialize(Dictionary<int, Entity> entityDict)
    {

    }

    public abstract void ActiveEntity(SummonData data);

    public virtual void DeactiveEntity(int index)
    {

    }
}
