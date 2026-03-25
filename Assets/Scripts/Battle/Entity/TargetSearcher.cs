using UnityEngine;
using System.Collections.Generic;

public class TargetSearcher : MonoBehaviour
{
    [SerializeField] private Platform platform;
    [SerializeField] private EnemyList enemyList;

    private List<Entity> availableTargets;
    private float range;

    private void Start()
    {
        availableTargets = new List<Entity>();
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
        availableTargets.Clear();
    }

    private void SerachTarget()
    {
        if (platform.EntityCount == 0) return;

        Entity tempTarget = null;

        foreach(var entity in EnemyList.Enemies)
        {
            if(Vector3.Distance(transform.position, entity.gameObject.transform.position) <= range)
            {
                if(tempTarget == null || Vector3.Distance(platform.TargetLastPosition, entity.transform.position) < Vector3.Distance(platform.TargetLastPosition, tempTarget.transform.position))
                {
                    tempTarget = entity;
                }
            }
        }

        availableTargets.Clear();
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
