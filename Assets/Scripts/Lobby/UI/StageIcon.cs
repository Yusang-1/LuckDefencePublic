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
        
        if(button == null) button = GetComponent<Button>();
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
            Color setColor;
            ColorUtility.TryParseHtmlString(unlockedColor, out setColor);
            iconImage.color = setColor;
            starImage.color = setColor;
            button.enabled = true;
        }
        else
        {
            Color setColor;
            ColorUtility.TryParseHtmlString(lockedColor, out setColor);
            iconImage.color = setColor;
            starImage.color = setColor;
            button.enabled = false;
        }
    }
}
