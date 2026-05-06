using UnityEngine;

[CreateAssetMenu(fileName = "Stages", menuName = "Scriptable Objects/Stage/StagesSO")]
public class StagesSO : ScriptableObject, ISaveData, IRewardDataGiver
{
    [SerializeField] private StageSO[] stages;
    
    int clearedStageIndex;
    public void StackStageClear(int index)
    {
        clearedStageIndex = -1;
        if(index >= stages.Length)
        {            
            Debug.LogWarning("잘못된 stage index : StageClear");
            return;
        }
        
        clearedStageIndex = index;
    }
    public void ApplyStageClear()
    {
        if(clearedStageIndex < 0) return;
        
        stages[clearedStageIndex].IsCleared = true;
    }
    
    public void StageUnlock(int index)
    {
        if(index >= stages.Length)
        {
            Debug.LogWarning("잘못된 stage index : StageUnlock");
            return;
        }
        
        stages[index].IsUnlocked = true;
    }

    public IDataStructForSave GetSaveData()
    {
        bool[] isCleared = new bool[stages.Length];
        bool[] isUnlocked = new bool[stages.Length];
        for(int i = 0; i < stages.Length; i++)
        {
            isCleared[i] = stages[i].IsCleared;
            isUnlocked[i] = stages[i].IsUnlocked;
        }
        
        IDataStructForSave saveData = new StageSaveData
        {
            IsCleared = isCleared,
            IsUnlocked = isUnlocked
        };
        
        return saveData;
    }    

    public void SetLoadData(IDataStructForSave loadData)
    {
        StageSaveData saveData = (StageSaveData)loadData;
        
        for(int i = 0; i < stages.Length; i++)
        {            
            stages[i].IsCleared = saveData.IsCleared[i];
            stages[i].IsUnlocked = saveData.IsUnlocked[i];
        }
        
        clearedStageIndex = -1;
    }
    
    public void SetDefaultData()
    {
        for(int i = 0; i < stages.Length; i++)
        {            
            stages[i].IsCleared = false;
            stages[i].IsUnlocked = i == 0;
        }
        
        clearedStageIndex = -1;
    }

    public void SetRewardInfo(RewardInfo info, int code)
    {
        info.SetData(stages[code].StageName, stages[code].Sprite);
    }

    public struct StageSaveData : IDataStructForSave
    {
        public bool[] IsCleared;
        public bool[] IsUnlocked;
    }
}
