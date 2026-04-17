using UnityEngine;
using System;
using System.Collections;

public abstract class Manager : MonoBehaviour
{
    // [NonSerialized] public bool isInitialized;
    
    // public virtual IEnumerator Initialize()
    // {
    //     if(isInitialized) yield break;
    // }
    
    
}

public interface IManagerSceneEntry
{
    public IEnumerator Initialize();
    
    public void DestroyPrevUIAfterLoad();
}
