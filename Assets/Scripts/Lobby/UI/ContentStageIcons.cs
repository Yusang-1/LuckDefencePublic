using System;
using UnityEngine;

public class ContentStageIcons : MonoBehaviour
{
    [SerializeField] private StageIcon[] stageIcons;
    private const float iconMargin = -380;
    private const float iconSpacing = 480;
    
    public void Initialize(Action<StageSO> onStageSelected)
    {
        float width = SetContentSize();
        SetStageIcons(onStageSelected, width);
    }
    
    private float SetContentSize()
    {
        float width = -iconMargin * 2 + iconSpacing * (stageIcons.Length - 1);
        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
        
        return width;
    }
    
    private void SetStageIcons(Action<StageSO> onStageSelected, float contentWidth)
    {
        for(int i = 0; i < stageIcons.Length; i++)
        {
            var stageIcon = Instantiate(stageIcons[i], transform);
            stageIcon.Initialize(onStageSelected);
            
            
            var rect = stageIcon.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(-contentWidth/2 + -iconMargin + i * iconSpacing, 0, 0);
        }
    }
}
