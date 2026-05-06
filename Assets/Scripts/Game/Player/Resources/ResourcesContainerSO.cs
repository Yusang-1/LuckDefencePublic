using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ResourcesContainer", menuName = "Scriptable Objects/Resources/ResourcesContainer")]
public class ResourcesContainerSO : ScriptableObject, IRewardDataGiver
{
    [SerializeField] private ResourcesSO[] resources;

    public Dictionary<ResourcesSO.ResourcesEnum, ResourcesSO> Datas;

    public void Initialize()
    {
        Datas = new Dictionary<ResourcesSO.ResourcesEnum, ResourcesSO>();        
        for (int i = 0; i < resources.Length; i++)
        {
            Datas.Add(resources[i].Code, resources[i]);
        }
    }

    public void SetRewardInfo(RewardInfo info, int resourcesCode)
    {
        ResourcesSO.ResourcesEnum code = (ResourcesSO.ResourcesEnum)resourcesCode;
        info.SetData(Datas[code].ResourcesName, Datas[code].Sprite);
    }
}
