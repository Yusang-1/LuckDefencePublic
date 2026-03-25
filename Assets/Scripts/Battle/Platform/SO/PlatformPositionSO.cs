using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlatformPositionSO", menuName = "Scriptable Objects/PlatformPositionSO")]
public class PlatformPositionSO : ScriptableObject
{
    [SerializeField] private PlatformPosition[] platformPosition;
    private Dictionary<CharRank, PlatformPosition> platformPosDict;

    public Dictionary<CharRank, PlatformPosition> PlatformPosDict => platformPosDict;

    public void Initialize()
    {
        platformPosDict = new Dictionary<CharRank, PlatformPosition>();
        foreach (var pp in platformPosition)
        {
            platformPosDict.Add(pp.Rank, pp);
        }
    }
}

[Serializable]
public struct PlatformPosition
{
    [SerializeField] private CharRank rank;
    [SerializeField] private int roomArea;
    [SerializeField] private Vector3[] pos;

    public CharRank Rank => rank;
    public int RoomArea => roomArea;
    public Vector3[] Pos => pos;
}

public enum CharRank
{
    none = -1,
    common = 0,
    rare,
    epic,
    unique,
    legendary
}
