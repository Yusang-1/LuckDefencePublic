using UnityEngine;

[CreateAssetMenu(fileName = "RankProbabilitySO", menuName = "Scriptable Objects/RankProbabilitySO")]
public class RankProbabilitySO : ScriptableObject
{
    [SerializeField] private float[] probability;

    public float[] Probabilities => probability;
}
