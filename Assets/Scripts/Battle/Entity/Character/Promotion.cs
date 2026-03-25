using UnityEngine;

public class Promotion : MonoBehaviour
{
    private Platforms platforms;
    private CharacterSpawner spawner;

    private PlatformData platformData;
    private bool isPromotionable;

    public bool IsPromotionable
    {
        get => isPromotionable;
        set
        {
            isPromotionable = value;
        }
    }

    private void Start()
    {
        platforms = FindFirstObjectByType<Platforms>();
        spawner = FindFirstObjectByType<CharacterSpawner>();
    }

    public void GetPlatformData(PlatformData data, bool isPormotionable)
    {
        platformData = data;
        IsPromotionable = isPormotionable;
    }

    public void OnPromotion()
    {
        spawner.PromotionEntity(platformData);
        IsPromotionable = false;
    }
}
