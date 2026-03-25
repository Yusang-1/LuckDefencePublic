using UnityEngine;

[CreateAssetMenu(fileName = "DistractMoveSkillEffect", menuName = "Scriptable Objects/Skill/DistractMoveSkillEffect")]
public class DistractMoveSkillEffect : MaintainSkillEffectSO
{
    public override ISkill CreateSkill(Entity subject)
    {
        return new DistractMove(this);
    }
}
