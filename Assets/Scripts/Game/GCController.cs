using UnityEngine;

public class GCController
{
    public static void CollectGarbage()
    {
        System.GC.Collect(0, System.GCCollectionMode.Forced);
    }
}
