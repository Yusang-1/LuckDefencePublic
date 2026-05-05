using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Rank Unlock SO", menuName = "Scriptable Objects/Player/Rank Unlock SO")]
public class RankUnlockSO : ScriptableObject, ISaveData
{
    [SerializeField] private bool[] isRankUnlocked;
    
    public void UnlockRank(CharRank rank)
    {
        if((int)rank >= isRankUnlocked.Length) return;
        
        isRankUnlocked[(int)rank] = true;
    }
    public void UnlockRank(int rank)
    {
        if(rank >= isRankUnlocked.Length) return;
        
        isRankUnlocked[rank] = true;
    }
    
    public bool IsRankUnlocked(CharRank rank)
    {
        return isRankUnlocked[(int)rank];
    }

    public IDataStructForSave GetSaveData()
    {
        IDataStructForSave saveData = new RankUnlockSaveData
        {
            IsRankUnlocked = isRankUnlocked.ToArray<bool>()
        };
        return saveData;
    }

    public void SetLoadData(IDataStructForSave loadData)
    {
        RankUnlockSaveData data = (RankUnlockSaveData)loadData;
        isRankUnlocked = data.IsRankUnlocked.ToArray<bool>();
    }

    public void SetDefaultData()
    {
        int count = 0;
        foreach(CharRank rank in Enum.GetValues(typeof(CharRank)))
        {
            if(rank == CharRank.none) continue;
            count++;
        }
        isRankUnlocked = new bool[count];
        isRankUnlocked[0] = true;
    }

    public struct RankUnlockSaveData : IDataStructForSave
    {
        public bool[] IsRankUnlocked;
    }
}
