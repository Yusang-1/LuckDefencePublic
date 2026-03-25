using UnityEngine;
using System;
using System.Collections;

public class BuffController : MonoBehaviour
{
    private Entity entity;

    public void Initialize(Entity entity)
    {
        this.entity = entity;
    }

    public void GetBuff(Func<Entity, float> applyBuff, Action<Entity> releaseBuff)
    {
        float length = applyBuff.Invoke(entity);
        
        if(gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitForEffectEnd(length, releaseBuff));            
        }
    }

    private IEnumerator WaitForEffectEnd(float length, Action<Entity> releaseBuff)
    {
        yield return new WaitForSeconds(length);

        releaseBuff.Invoke(entity);
    }
}
