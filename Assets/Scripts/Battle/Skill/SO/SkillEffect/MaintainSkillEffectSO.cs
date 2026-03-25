using UnityEngine;

[CreateAssetMenu(fileName = "MaintainSkillEffectSO", menuName = "Scriptable Objects/Skill/MaintainSkillEffectSO")]
public class MaintainSkillEffectSO : SkillEffectSO
{
    [SerializeField] private float effectLength;
    
    public float EffectLength => effectLength;
}
