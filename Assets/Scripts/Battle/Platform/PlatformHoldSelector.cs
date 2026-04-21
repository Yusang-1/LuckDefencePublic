using System.Collections;
using UnityEngine;

public class PlatformHoldSelector : MonoBehaviour
{
    [SerializeField] private Platforms platforms;
    [SerializeField] private GameObject selectEffect;
    [SerializeField] private GameObject holdEffect;
    [SerializeField] private GameObject releseEffect;

    private int holdedIndex;
    private int releasedIndex;

    void Update()
    {
        //DrawLine();
    }

    public void Initialize()
    {
        holdedIndex = -1;
        releasedIndex = -1;
    }
    
    public void Selected(int platformIndex, Platform platform)
    {
        selectEffect.transform.position = platform.transform.position;
        selectEffect.SetActive(true);
    }
    
    public void SelectEnd()
    {
        selectEffect.SetActive(false);
    }

    public void Holded(int platformIndex, Platform platform)
    {
        holdedIndex = platformIndex;
        platforms.SelectedPlatformIndex = -1;
        platforms.SelectedPlatformIndex = -1;
        
        holdEffect.transform.position = platform.transform.position;
        holdEffect.SetActive(true);
    }

    public void Released(int platformIndex, Platform platform)
    {
        releasedIndex = platformIndex;
        
        holdEffect.SetActive(false);
        releseEffect.transform.position = platform.transform.position;
        releseEffect.SetActive(true);
        StartCoroutine(WaitEffect(0.5f));
        DoJob();
    }
    
    private IEnumerator WaitEffect(float time)
    {
        yield return new WaitForSeconds(time);
        releseEffect.SetActive(false);
    }

    private void DoJob()
    {
        if(holdedIndex == releasedIndex || holdedIndex == -1 || releasedIndex == -1)
        {
            return;
        }        

        Entity[] entities = platforms.PlatformList[holdedIndex].Entities;
        foreach(Entity entity in entities)
        {
            if(entity != null)
            {
                entity.Mover.GetDestinationVector(platforms.PlatformList[releasedIndex].transform.position);
                entity.Mover.GetDestinationVector(platforms.PlatformList[releasedIndex].GetPosition((entity.Data as CharacterSO).Rank));
                entity.Mover.Move();

                platforms.PlatformList[releasedIndex].EntitySpawned(entity.gameObject);
            }
        }

        platforms.PlatformList[holdedIndex].Migration();
    }
}
