using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RankProbabilitySO", menuName = "Scriptable Objects/RankProbabilitySO")]
public class RankProbabilitySO : ScriptableObject
{
    [SerializeField] private float[] probabilties;
    private Dictionary<CharRank, float> probabilityDict;
    
    public Dictionary<CharRank, float> ProbabilityDict => probabilityDict;

    public void Initialize()
    {
        probabilityDict = new Dictionary<CharRank, float>();

        for (int i = 0; i < probabilties.Length; i++)
        {
            probabilityDict.Add((CharRank)i, probabilties[i]);
        }
    }
}
