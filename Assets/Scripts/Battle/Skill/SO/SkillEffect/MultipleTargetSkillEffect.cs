using UnityEngine;

[CreateAssetMenu(fileName = "MultipleTargetSkillEffect", menuName = "Scriptable Objects/Skill/MultipleTargetSkillEffect")]
public class MultipleTargetSkillEffect : SkillEffectSO
{
    [SerializeField] private float hitRange;
    
    public float HitRange => hitRange;
    
    public override ISkill CreateSkill(Entity subject)
    {
        return new HitMultipleTarget(this, subject);
    }
}
