using UnityEngine;
using System;
using System.Collections.Generic;

public class RewardHandler : MonoBehaviour
{
    public event Action<int> OnGetRewardCoin;
    public event Action<int> OnGetRewardUnlockStage;
    public event Action<int> OnGetRewardUnlockRank;
    public event Action<int> OnGetRewardCharacter;
    
    private Dictionary<RewardType, Action<int>> rewardHandlerDect;

    private void Start()
    {
        rewards = null;
        rewardHandlerDect = new Dictionary<RewardType, Action<int>>();
        rewardHandlerDect.Add(RewardType.property, OnGetRewardCoin);
        rewardHandlerDect.Add(RewardType.unlockStage, OnGetRewardUnlockStage);
        rewardHandlerDect.Add(RewardType.unlockRank, OnGetRewardUnlockRank);
        rewardHandlerDect.Add(RewardType.character, OnGetRewardCharacter);
    }
    
    RewardData[] rewards;
    public void StackReward(RewardData[] rewards)
    {
        this.rewards = rewards;
    }
    
    public void TryGetReward()
    {
        if(rewards == null || rewards.Length == 0) return;
        
        RewardData reward;
        for(int i = 0; i < rewards.Length; i++)
        {
            reward = rewards[i];
            rewardHandlerDect[reward.Type]?.Invoke(reward.Value);
        }
    }
}
