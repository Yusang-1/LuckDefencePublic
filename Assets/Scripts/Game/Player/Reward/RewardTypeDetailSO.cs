using System;

[Serializable]
public struct RewardData
{
    public RewardType Type;
    public int Value;
    public int Count;
}

public enum RewardType
{
    property,
    unlockStage,
    unlockRank,
    character
}
