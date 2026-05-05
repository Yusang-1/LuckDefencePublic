using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RewardTypeDetail", menuName = "Scriptable Objects/RewardTypeDetail")]
public class RewardTypeDetailSO : ScriptableObject
{
    
}

[Serializable]
public struct RewardData
{
    public RewardType Type;
    public int Value;
}

public enum RewardType
{
    coin,
    unlockStage,
    unlockRank,
    character
}
