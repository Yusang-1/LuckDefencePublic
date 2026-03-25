using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : EntitySO
{
    [SerializeField] private EnemyRank rank;
    [SerializeField] private int dropCoin;

    public EnemyRank Rank => rank;
    public int DropCoin => dropCoin;
}

public enum EnemyRank
{
    common,
    boss
}