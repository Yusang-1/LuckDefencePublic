using UnityEngine;

[CreateAssetMenu(fileName = "ShootProjectileSkillEffectSO", menuName = "Scriptable Objects/Skill/ShootProjectileSkillEffectSO")]
public class ShootProjectileSkillEffect : SkillEffectSO
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int poolCount;
    [SerializeField] private SkillEffectSO[] hitEffects;

    public GameObject ProjectilePrefab => projectilePrefab;
    public float ProjectileSpeed => projectileSpeed;
    public int PoolCount => poolCount;
    public SkillEffectSO[] HitEffects => hitEffects;

    public override ISkill CreateSkill(Entity subject)
    {
        return new ShootProjectile(this, subject);
    }
}
