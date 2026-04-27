using UnityEngine;
using System;

public class StageSelectUI : MonoBehaviour, ILobbyUIState
{
    public event Action<StageSO> StageSelected;
    
    [SerializeField] private ContentStageIcons contentStageIcons;
    private StageSO selectedStage;
    private StageSO selectedStageProperty
    {
        get { return selectedStage; }
        set
        {
            selectedStage = value;
            StageSelected?.Invoke(selectedStage);
        }
    }
        
    public void Initialize()
    {
        contentStageIcons.Initialize(OnStageSelected);
    }
    
    private void OnStageSelected(StageSO stageData)
    {
        selectedStageProperty = stageData;
    }

    public void ActiveUI()
    {
        gameObject.SetActive(true);
    }

    public void DeactiveUI()
    {
        gameObject.SetActive(false);
    }
}
