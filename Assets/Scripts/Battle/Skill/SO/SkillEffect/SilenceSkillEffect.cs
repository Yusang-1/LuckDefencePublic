using UnityEngine;

[CreateAssetMenu(fileName = "SilenceSkillEffect", menuName = "Scriptable Objects/Skill/SilenceSkillEffect")]
public class SilenceSkillEffect : MaintainSkillEffectSO
{
    public override ISkill CreateSkill(Entity subject)
    {
        return new Silence(this);
    }
}
