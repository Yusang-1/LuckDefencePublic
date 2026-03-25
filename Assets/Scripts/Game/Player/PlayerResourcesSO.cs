using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Scriptable Objects/Player Resources")]
public class PlayerResourcesSO : ScriptableObject, ISaveData
{
    public event Action ResourcesChanged;

    [SerializeField] private int playerLevel;
    [SerializeField] private string playerName;
    [SerializeField] private int playerCoin;

    public int PlayerLevel => playerLevel;
    public string PlayerName => playerName;
    public int PlayerCoin => playerCoin;

    public void ChangePlayerLevel(int level)
    {
        playerLevel = level;
        ResourcesChanged?.Invoke();
    }

    public void AddPlayerCoin(int coin)
    {
        playerCoin = Mathf.Clamp(playerCoin+coin, 0, playerCoin + coin);
        ResourcesChanged?.Invoke();
    }

    public void ChangePlayerName(string name)
    {
        playerName = name;
        ResourcesChanged?.Invoke();
    }
    
    public void AddReward(RewardData rewardData)
    {
        AddPlayerCoin(rewardData.CoinReward);
        // 보석 로직 추가시 구현 필요
    }

    public IDataStructForSave GetSaveData()
    {
        PlayerResourcesSaveData saveData = new PlayerResourcesSaveData
        {
            playerLevel = playerLevel,
            playerName = playerName,
            playerCoin = playerCoin
        };

        return saveData;
    }

    public void SetLoadData(IDataStructForSave loadData)
    {
        PlayerResourcesSaveData saveData = (PlayerResourcesSaveData)loadData;
        playerLevel = saveData.playerLevel;
        playerName = saveData.playerName;
        playerCoin = saveData.playerCoin;
        
        ResourcesChanged?.Invoke();
    }
    
    public void SetDefaultData()
    {
        playerLevel = 1;
        playerName = "Player";
        playerCoin = 100;
        
        ResourcesChanged?.Invoke();
    }
    
    public struct PlayerResourcesSaveData : IDataStructForSave
    {
        public int playerLevel;
        public string playerName;
        public int playerCoin;
    }
}
