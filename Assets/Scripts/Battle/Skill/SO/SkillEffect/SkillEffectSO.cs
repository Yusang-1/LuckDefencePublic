using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffectSO", menuName = "Scriptable Objects/Skill/SkillEffectSO")]
public class SkillEffectSO : ScriptableObject
{
    [SerializeField] private float value;

    public float Value => value;
    
    public virtual ISkill CreateSkill(Entity subject)
    {
        return new HitSingleTarget(this, subject);
    }
}
