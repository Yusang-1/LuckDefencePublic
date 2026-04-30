using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "RankProbabilitySO", menuName = "Scriptable Objects/RankProbabilitySO")]
public class RankProbabilitySO : ScriptableObject
{
    [SerializeField] private float[] probabilties;
    [SerializeField] private RankUnlockSO rankUnlockData;
    
    private Dictionary<CharRank, float> probabilityDict;
    public Dictionary<CharRank, float> ProbabilityDict => probabilityDict;

    public void Initialize()
    {
        MakeBattleProbabilityDict();
    }
    
    private void MakeBattleProbabilityDict()
    {
        probabilityDict = new Dictionary<CharRank, float>();
        
        foreach(CharRank rank in Enum.GetValues(typeof(CharRank)))
        {
            if(rank == CharRank.none) continue;
            
            if(rankUnlockData.IsRankUnlocked(rank) == false)
            {
                probabilityDict[CharRank.common] += probabilties[(int)rank];
                probabilityDict.Add(rank, 0);
            }
            else
                probabilityDict.Add(rank, probabilties[(int)rank]);
        }
    }
}
