using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterListDataSO", menuName = "Scriptable Objects/CharacterList/CharacterListDataSO")]
public class CharacterListDataSO : ScriptableObject
{
    [SerializeField] protected CharListAsRank[] charListAsRanks;
    protected Dictionary<CharRank, CharListAsRank> charListAsRankDictionary;

    public Dictionary<CharRank, CharListAsRank> CharListAsRankDictionary => charListAsRankDictionary;

    public bool IsDirty;

    public virtual void Initialize()
    {
        IsDirty = false;
        charListAsRankDictionary = new Dictionary<CharRank, CharListAsRank>();

        for (int i = 0; i < charListAsRanks.Length; i++)
        {
            charListAsRankDictionary.Add(charListAsRanks[i].Rank, charListAsRanks[i]);

            charListAsRanks[i].Initialize();
        }
    }
}
