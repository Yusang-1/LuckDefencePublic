using UnityEngine;
using System.Collections.Generic;

public class FactoryCharLegendary : FactoryChar
{
    public override void Initialize(Dictionary<int, Entity> entityDict)
    {
        base.Initialize(entityDict);
    }


    public override void DeactiveEntity(int index)
    {
        base.DeactiveEntity(index);
    }
}