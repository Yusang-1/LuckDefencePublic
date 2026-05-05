using UnityEngine;
using UnityEngine.UI;
using System;

public class StageIcon : MonoBehaviour
{
    [SerializeField] private StageSO stageData;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image starImage;
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private Button button;
    private Action<StageSO> onStageSelected;

    public void Initialize(Action<StageSO> onStageSelected)
    {
        this.onStageSelected = onStageSelected;
        
        stageData.OnCleared += Cleared;
        stageData.OnUnlocked += Unlocked;
        
        Cleared(stageData.IsCleared);
        Unlocked(stageData.IsUnlocked);
    }

    private void OnDestroy()
    {
        stageData.OnCleared -= Cleared;
        stageData.OnUnlocked -= Unlocked;
    }

    public void OnClick()
    {
        if(stageData.IsUnlocked == false) return;
        
        onStageSelected?.Invoke(stageData);
    }
    
    private void Cleared(bool value)
    {
        if (value)
        {
            scoreContainer.SetActive(true);
        }
        else 
            scoreContainer.SetActive(false);
    }
    
    private const string lockedColor = "#797979";
    private const string unlockedColor = "#ffffff";
    private void Unlocked(bool value)
    {
        if(value)
        {
            ColorUtility.TryParseHtmlString(unlockedColor, out Color setColor);
            iconImage.color = setColor;
            starImage.color = setColor;
            button.enabled = true;
        }
        else
        {
            ColorUtility.TryParseHtmlString(lockedColor, out Color setColor);
            iconImage.color = setColor;
            starImage.color = setColor;
            button.enabled = false;
        }
    }
}
