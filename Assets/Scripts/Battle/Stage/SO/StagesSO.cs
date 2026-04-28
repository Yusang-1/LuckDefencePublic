using UnityEngine;

[CreateAssetMenu(fileName = "Stages", menuName = "Scriptable Objects/Stage/StagesSO")]
public class StagesSO : ScriptableObject, ISaveData
{
    [SerializeField] private StageSO[] stages;
    
    public void StageClear(int index)
    {
        stages[index].IsCleared = true;
        if(index+1 < stages.Length)
            stages[index+1].IsUnlocked = true;
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
    }
    
    public void SetDefaultData()
    {
        for(int i = 0; i < stages.Length; i++)
        {            
            stages[i].IsCleared = false;
            stages[i].IsUnlocked = i == 0;
        }
    }

    public struct StageSaveData : IDataStructForSave
    {
        public bool[] IsCleared;
        public bool[] IsUnlocked;
    }
}
