using UnityEngine;
using System.Collections.Generic;

public class RewardSpriteHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private Dictionary<RewardType, RewardSpriteGetter> spriteGetter;

    private void Start()
    {
        spriteGetter = new Dictionary<RewardType, RewardSpriteGetter>();
        spriteGetter.Add(RewardType.character, new RewardCharacterSpriteGetter(gameManager.CharacterData));
        spriteGetter.Add(RewardType.property, new RewardResourcesSpirteGetter());
        spriteGetter.Add(RewardType.unlockStage, new RewardUnlockStageSpirteGetter());
        spriteGetter.Add(RewardType.unlockRank, new RewardUnlockRankSpirteGetter());
    }
    
    public Sprite GetSprite(RewardData rewardData)
    {
        return spriteGetter[rewardData.Type].GetSprite(rewardData.Value);
    }
}

public abstract class RewardSpriteGetter
{    
    public abstract Sprite GetSprite(int code);
}

public class RewardCharacterSpriteGetter : RewardSpriteGetter
{
    private readonly CharacterData characterData;
    
    public RewardCharacterSpriteGetter(CharacterData characterData)
    {
        this.characterData = characterData;
    }
    
    public override Sprite GetSprite(int code)
    {
        CharRank rank = characterData.GetCharRankByCode(code);
        return (characterData.CharacterListData.CharListAsRankDictionary[rank].EntityAsCodeDict[code].Data as CharacterSO).Portrait;
    }
}

public class RewardResourcesSpirteGetter : RewardSpriteGetter
{
    
    public override Sprite GetSprite(int code)
    {
        return null;
    }
}

public class RewardUnlockStageSpirteGetter : RewardSpriteGetter
{
    
    
    public override Sprite GetSprite(int code)
    {
        
        return null;
    }
}

public class RewardUnlockRankSpirteGetter : RewardSpriteGetter
{
    
    
    public override Sprite GetSprite(int code)
    {
        
        return null;
    }
}
