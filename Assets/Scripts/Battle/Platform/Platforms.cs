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
    [SerializeField] private RankUnlockSO rankUnlockData;

    private int selectedPlatformIndex;

    public Platform[] PlatformList => platformList;
    public InputPlatform InputPlatform;
    public RankUnlockSO RankUnlockData => rankUnlockData;
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

    private List<int> availableIndexes;
    private List<int> availableIndexesHaveCharCode;
    const int haveCharCodeProbability = 75;
    public int CheckAvailablePlatformIndexByCharacter(int charCode)
    {
        if (availableIndexes == null)
            availableIndexes = new List<int>();
        else
            availableIndexes.Clear();

        if (availableIndexesHaveCharCode == null)
            availableIndexesHaveCharCode = new List<int>();
        else
            availableIndexesHaveCharCode.Clear();

        // 해당 캐릭터가 들어갈 수 있는 플렛폼을 판별
        Platform platform;
        for (int i = 0; i < platformList.Length; i++)
        {
            platform = platformList[i];
            if (platform.CheckEntityAvailable(charCode))
            {
                if (platform.EntityCount == 0)
                {
                    if (!availableIndexes.Contains(platform.Index))
                    {
                        availableIndexes.Add(platform.Index);
                    }
                }
                else
                {
                    if (platform.Entities[0].Data.Code != charCode) // 플렛폼에 다른 entity가 있는 경우
                    {
                        if (!availableIndexes.Contains(platform.Index))
                        {
                            availableIndexes.Add(platform.Index);
                        }
                    }
                    else // 플렛폼에 같은 entity가 있는 경우
                    {
                        if (!availableIndexesHaveCharCode.Contains(platform.Index))
                        {
                            availableIndexesHaveCharCode.Add(platform.Index);
                        }
                    }
                }
            }
        }

        int randNum;
        if (availableIndexes.Count > 0 && availableIndexesHaveCharCode.Count > 0)
        {
            randNum = UnityEngine.Random.Range(1, 101);
            if (randNum <= haveCharCodeProbability)
            {
                randNum = UnityEngine.Random.Range(0, availableIndexesHaveCharCode.Count);
                return availableIndexesHaveCharCode[randNum];
            }
            else
            {
                randNum = UnityEngine.Random.Range(0, availableIndexes.Count);
                return availableIndexes[randNum];
            }
        }
        else if (availableIndexes.Count > 0 && availableIndexesHaveCharCode.Count == 0)
        {
            randNum = UnityEngine.Random.Range(0, availableIndexes.Count);
            return availableIndexes[randNum];
        }
        else if (availableIndexesHaveCharCode.Count > 0 && availableIndexes.Count == 0)
        {
            randNum = UnityEngine.Random.Range(0, availableIndexesHaveCharCode.Count);
            return availableIndexesHaveCharCode[randNum];
        }
        else
        {
            Debug.LogWarning("availableIndexes와 availableIndexesHaveCharCode의 count가 모두 0");
            return -1;
        }
    }

    public Vector3 GetSummonPosition(int index, CharRank rank) => platformList[index].GetPosition(rank);

    public void ResetPlatform(int index) => platformList[index].ResetPlatform();

    public void EntitySpawned(int index, GameObject go) => platformList[index].EntitySpawned(go);

    public void Migration(int index) => platformList[index].Migration();
}