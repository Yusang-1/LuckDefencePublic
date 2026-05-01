using UnityEngine;
using System;
using System.Collections.Generic;

public class Platforms : MonoBehaviour
{
    public event Action<Platform> PlatformDataChanged;
    public event Action<Platform> PlatformSelected;
    public event Action NoPlatformSelected;

    [SerializeField] private Platform[] platformList;
    [SerializeField] private PlatformHoldSelector holdSelector;
    [SerializeField] private PlatformHoldArrowDrawer arrowDrawer;

    private int selectedPlatformIndex;

    public Platform[] PlatformList => platformList;
    public InputPlatform InputPlatform;

    public int SelectedPlatformIndex
    {
        get => selectedPlatformIndex;
        set
        {
            if (selectedPlatformIndex >= 0 && value < 0)
            {
                selectedPlatformIndex = value;
                NoPlatformSelected?.Invoke();
                return;
            }

            if (selectedPlatformIndex < 0 && value < 0)
            {
                return;
            }

            selectedPlatformIndex = value;

            if (value >= 0 && platformList[value].EntityCount > 0)
            {
                PlatformSelected?.Invoke(platformList[value]);
            }
        }
    }

    public PlatformHoldSelector HoldSelector => holdSelector;
    public PlatformHoldArrowDrawer ArrowDrawer => arrowDrawer;

    private void Start()
    {
        selectedPlatformIndex = -1;

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].GetIndex(i);
        }
    }

    public void DataChanged(int index, bool isReset = false)
    {
        if (index != selectedPlatformIndex) return;

        if (isReset)
        {
            platformList[SelectedPlatformIndex].SelectedEnd();
            return;
        }

        PlatformDataChanged?.Invoke(platformList[index]);
    }

    List<int> availablePlatformIndexes;
    public int CheckAvailablePlatformIndexByCharacter(int charCode)
    {
        if (availablePlatformIndexes == null)
        {
            availablePlatformIndexes = new List<int>();
        }
        else
            availablePlatformIndexes.Clear();

        bool isAvailable;

        // 해당 캐릭터가 들어갈 수 있는 플렛폼을 판별
        foreach (Platform platform in platformList)
        {
            isAvailable = platform.CheckEntityAvailable(charCode);

            if (isAvailable && !availablePlatformIndexes.Contains(platform.Index))
            {
                availablePlatformIndexes.Add(platform.Index);
            }
        }

        int randNum = UnityEngine.Random.Range(0, availablePlatformIndexes.Count);

        return availablePlatformIndexes[randNum];
    }
    
    public Vector3 GetSummonPosition(int index, CharRank rank) => platformList[index].GetPosition(rank);
    
    public void ResetPlatform(int index) => platformList[index].ResetPlatform();
    
    public void EntitySpawned(int index, GameObject go) => platformList[index].EntitySpawned(go);
    
    public void Migration(int index) => platformList[index].Migration();
}