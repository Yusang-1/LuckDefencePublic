using UnityEngine;
using System;

public class Platforms : MonoBehaviour
{
    public event Action<Platform> PlatformDataChanged;
    public event Action<Platform> PlatformSelected;
    public event Action NoPlatformSelected;

    [SerializeField] private Platform[] platformList;    
    
    private int selectedPlatformIndex;

    public Platform[] PlatformList => platformList;    

    public int SelectedPlatformIndex
    {
        get => selectedPlatformIndex;
        set
        {
            if(selectedPlatformIndex >= 0 && value < 0)
            {                
                selectedPlatformIndex = value;
                return;
            }

            if(selectedPlatformIndex < 0 && value < 0)
            {
                NoPlatformSelected?.Invoke();
                return;
            }

            selectedPlatformIndex = value;
            
            if(value >= 0 && platformList[value].EntityCount > 0)
            {
                PlatformSelected?.Invoke(platformList[value]);
            }
        }
    }

    private void Start()
    {
        selectedPlatformIndex = -1;

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].GetIndex(i);
        }
    }

    public void DataChanged(int index)
    {
        if (index != selectedPlatformIndex) return;

        PlatformDataChanged?.Invoke(platformList[index]);
    }
}