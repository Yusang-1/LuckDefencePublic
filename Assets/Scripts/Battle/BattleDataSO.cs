using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "Scriptable Objects/BattleData")]
public class BattleDataSO : ScriptableObject
{
    public event Action<RoundData> StartNextRound;
    public event Action<int> EnemyCountChanged;
    public event Action EnemyFull;
    public event Action AllEnemyDied;
    public event Action<int> CoinChanged;
    public event Action NotEnoughCoin;
    public event Action EnoughCoin;

    [SerializeField] private int roundNum;

    [SerializeField] private int currentEnemyCount;

    [SerializeField] private int spawnCost;

    private int currentCoin;

    private StageSO stageData;

    public StageSO StageData => stageData;

    public int CurrentCoin
    {
        get => currentCoin;
        set
        {
            if(value < spawnCost)
            {
                NotEnoughCoin?.Invoke();
            }

            if(currentCoin < spawnCost && value >= spawnCost)
            {
                EnoughCoin?.Invoke();
            }

            currentCoin = Mathf.Clamp(value, 0, value);            

            CoinChanged?.Invoke(currentCoin);
        }
    }

    public int SpawnCost => spawnCost;

    public int RoundNum
    {
        get => roundNum;
        set
        {
            roundNum = value;
            if(roundNum == 0)
            {
                CurrentCoin = stageData.InitialCoin;
            }

            if(roundNum >= 0)
            {
                StartNextRound?.Invoke(stageData.RoundData[roundNum]);
            }
        }
    }

    public int CurrentEnemyCount
    {
        get => currentEnemyCount;
        set
        {
            if (currentEnemyCount > stageData.MaxEnemyCount + 1)
            {
                return;
            }

            currentEnemyCount = value;
            EnemyCountChanged?.Invoke(currentEnemyCount);

            if (currentEnemyCount > stageData.MaxEnemyCount)
            {
                EnemyFull?.Invoke();
            }

            if(roundNum == stageData.RoundCount - 1 && currentEnemyCount == 0)
            {
                AllEnemyDied?.Invoke();
            }
        }
    }

    public bool IsGameOver;

    public void Initialize(StageSO stageData)
    {
        this.stageData = stageData;
        OnResetData();
    }

    public void OnResetData()
    {
        IsGameOver = false;
        currentEnemyCount = 0;
        RoundNum = -1;
        CurrentCoin = stageData.InitialCoin;
    }

    private void OnEnemyDied()
    {
        currentEnemyCount--;
    }
}
