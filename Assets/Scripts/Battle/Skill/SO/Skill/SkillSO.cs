using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/Skill/SkillSO")]
public class SkillSO : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;
    [SerializeField] private Sprite sprite;  
    [SerializeField] private SkillEffectSO[] effectDatas;
    
    public string SkillName => skillName;
    public string SkillDescription => skillDescription;
    public Sprite Sprite => sprite;
    public SkillEffectSO[] EffectDatas => effectDatas;
}
