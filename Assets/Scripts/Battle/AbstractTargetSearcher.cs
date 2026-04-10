using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTargetSearcher
{
    protected List<Entity> targetPool;
    protected Vector3 standardPosition;
    protected float range;
    protected bool isSearching;
    
    public virtual void SetSearcher(List<Entity> targetPool, Vector3 standardPosition, float range)
    {
        this.targetPool = targetPool;
        this.standardPosition = standardPosition;
        this.range = range;
        isSearching = true;
    }    
    
    public abstract List<Entity> SearchTargets();
}
