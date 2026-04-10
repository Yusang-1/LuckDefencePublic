using UnityEngine;
using System.Collections.Generic;

public class PlatformTargetSearcher : MonoBehaviour
{
    [SerializeField] private Platform platform;
    [SerializeField] private EnemyList enemyList;

    private AbstractTargetSearcher searcher;
    private float range;

    private void Start()
    {
        searcher = new TargetSearcherInRange();
    }

    private void Update()
    {
        if(platform.Target == null)
        {
            SerachTarget();
        }
        else
        {
            TrackTarget();
        }
    }

    public void Initialize(Character character)
    {
        range = character.Data.AttackRange;
        searcher.SetSearcher(EnemyList.Enemies, transform.position, range);
    }

    private void SerachTarget()
    {
        if (platform.EntityCount == 0) return;

        Entity tempTarget = null;
        List<Entity> availableEntity = searcher.SearchTargets();
        
        foreach(var entity in availableEntity)
        {            
            if(tempTarget == null || Vector3.Distance(platform.TargetLastPosition, entity.transform.position) < Vector3.Distance(platform.TargetLastPosition, tempTarget.transform.position))
            {
                tempTarget = entity;
            }            
        }

        platform.SetTarget(tempTarget);
    }

    private void TrackTarget()
    {
        if(Vector3.Distance(transform.position, platform.Target.transform.position) > range)
        {
            platform.SetTarget(null);
        }
    }
}
