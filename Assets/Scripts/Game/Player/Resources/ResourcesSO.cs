using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesData", menuName = "Scriptable Objects/Resources/ResourcesData")]
public class ResourcesSO : ScriptableObject
{
    [SerializeField] private ResourcesEnum code;
    [SerializeField] private string resourcesName;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;

    public ResourcesEnum Code => code;
    public string ResourcesName => resourcesName;
    public string Description => description;
    public Sprite Sprite => sprite;

    public enum ResourcesEnum
    {
        coin
    }
}


