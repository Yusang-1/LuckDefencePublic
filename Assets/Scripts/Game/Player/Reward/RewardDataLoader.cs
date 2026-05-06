using System.Collections.Generic;

public class RewardDataLoader
{
    private Dictionary<RewardType, IRewardDataGiver> getter;
    
    public RewardDataLoader(GameManager gameManager)
    {
        Initialize(gameManager);
    }
    
    private void Initialize(GameManager gameManager)
    {        
        getter = new Dictionary<RewardType, IRewardDataGiver>();
        getter.Add(RewardType.property, gameManager.ResourcesData);
        getter.Add(RewardType.unlockStage, gameManager.StagesData);
        getter.Add(RewardType.unlockRank, gameManager.RankUnlockData);
        getter.Add(RewardType.character, gameManager.CharacterData);
    }
    
    public void SetRewardInfo(RewardData reward, RewardInfo info)
    {
        getter[reward.Type].SetRewardInfo(info, reward.Value);
    }
}
