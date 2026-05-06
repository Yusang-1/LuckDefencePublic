using UnityEngine;

public interface IRewardDataGiver
{
    public void SetRewardInfo(RewardInfo info, int code);
}

public class RewardInfo
{
    private string rewardName;
    private Sprite sprite;
    
    public string RewardName => rewardName;
    public Sprite Sprite => sprite;
    
    public void SetData(string name, Sprite sprite)
    {
        rewardName = name;
        this.sprite = sprite;
    }
}
