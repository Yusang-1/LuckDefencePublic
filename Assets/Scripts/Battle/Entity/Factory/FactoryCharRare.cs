using UnityEngine;
using System.Collections.Generic;

public class FactoryCharRare : FactoryChar
{
    public override void Initialize(Dictionary<int, Entity> entityDict)
    {        
        base.Initialize(entityDict);
    }

    public override void DeactiveEntity(int code)
    {
        base.DeactiveEntity(code);
    }
}
