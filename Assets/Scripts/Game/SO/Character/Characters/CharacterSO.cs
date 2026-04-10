using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : EntitySO
{
    public CharRank Rank;
    public int poolNum;
    public int Weight;
    public int price;
    
    public SkillData AttackData;
    public SkillData SkillData;
}
