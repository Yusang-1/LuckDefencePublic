using System;
using UnityEngine;

public class StageIcon : MonoBehaviour
{
    [SerializeField] private StageSO stageData;
    private Action<StageSO> onStageSelected;
    
    public void Initialize(Action<StageSO> onStageSelected)
    {
        this.onStageSelected = onStageSelected;
    }
    
    public void OnClick()
    {
        Debug.Log("Click");
        onStageSelected?.Invoke(stageData);
    }
}
