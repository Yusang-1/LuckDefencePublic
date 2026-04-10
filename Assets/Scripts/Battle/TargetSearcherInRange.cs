using System.Collections.Generic;
using UnityEngine;

public class TargetSearcherInRange : AbstractTargetSearcher
{   
    protected List<Entity> targets;

    public override void SetSearcher(List<Entity> targetPool, Vector3 standardPosition, float range)
    {
        base.SetSearcher(targetPool, standardPosition, range);
        targets = new List<Entity>();
    }
    
    public override List<Entity> SearchTargets()
    {
        targets.Clear();
        foreach(var entity in targetPool)
        {
            if(Vector3.Distance(standardPosition, entity.transform.position) <= range)
            {                
                targets.Add(entity);            
            }
        }
        return targets;
    }
}
