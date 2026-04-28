using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Objects/Stage/StageSO")]
public class StageSO : ScriptableObject
{
    public event Action<bool> OnCleared;
    public event Action<bool> OnUnlocked;
    
    [SerializeField] private int stageIndex;    
    [SerializeField] private bool isCleared;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private int stageNum;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private int initialCoin;
    [SerializeField] private RoundData[] roundData;
    [SerializeField] private RewardData rewardData;
    
    public int StageIndex => stageIndex;
    public int StageNum => stageNum;
    public int MaxEnemyCount => maxEnemyCount;
    public RoundData[] RoundData => roundData;
    public int RoundCount => roundData.Length;
    public int InitialCoin => initialCoin;
    public RewardData RewardData => rewardData;
    public bool IsCleared
    {
        get => isCleared;
        set
        {
            isCleared = value;
            OnCleared?.Invoke(value);
        }
    }
    public bool IsUnlocked
    {
        get => isUnlocked;
        set
        {
            isUnlocked = value;
            OnUnlocked?.Invoke(value);
        }
    }
}

[Serializable]
public struct RoundData
{
    [SerializeField] private Entity enemy;
    [SerializeField] private int enemyCount;
    [SerializeField] private int additionalTime;
    [SerializeField] private float spawnDelay;

    public Entity Enemy => enemy;
    public int EnemyCount => enemyCount;
    public int AdditionalTime => additionalTime;
    public float SpawnDelay => spawnDelay;
}

[Serializable]
public struct RewardData
{
    public int CoinReward;
    public int GemReward;
}
