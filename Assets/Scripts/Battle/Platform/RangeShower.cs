using UnityEngine;

public class RangeShower : MonoBehaviour
{
    [SerializeField] private GameObject shower;
    [SerializeField] private Platforms platforms;

    private void Start()
    {
        platforms.PlatformSelected += ActiveRangeShower;
        platforms.NoPlatformSelected += DeactiveRangeShower;
        shower.SetActive(false);
    }

    private void OnDestroy()
    {
        platforms.PlatformSelected -= ActiveRangeShower;
        platforms.NoPlatformSelected -= DeactiveRangeShower;
    }

    public void ActiveRangeShower(Platform platform)
    {
        if (platform.Entities[0] == null)
        {
            return;
        }

        Vector3 position = platform.gameObject.transform.position;
        float range = (platform.Entities[0].Data as CharacterSO).AttackRange;

        shower.transform.position = position;
        shower.transform.localScale = new Vector3(range*2, range*2);

        shower.SetActive(true);
    }

    public void DeactiveRangeShower()
    {
        shower.SetActive(false);
    }
}
