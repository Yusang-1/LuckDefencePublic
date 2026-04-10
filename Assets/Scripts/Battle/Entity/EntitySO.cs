using UnityEngine;

[CreateAssetMenu(fileName = "EntitySO", menuName = "Scriptable Objects/EntitySO")]
public class EntitySO : ScriptableObject
{
    public int Code;
    public string EntityName;
    public int MaxHp;
    public int MaxMp;
    public int GetMPPoint;
    public int AttackPoint;
    public float AttackSpeed;
    public float AttackRange;
    public float defaultDefencePoint;
    public float MoveSpeed;
    public GameObject Prefab;
}
