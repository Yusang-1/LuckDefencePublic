using System;
using System.Collections.Generic;

public class SummonInfo
{
    public List<CharRank> SummonableRanks;
    private readonly Dictionary<CharRank, Entity[]> summonableEntities;
    private readonly Dictionary<CharRank, float> summonProbability;

    public SummonInfo(RankUnlockSO rankUnlockData, CharacterListDataSO selectedCharacterData, RankProbabilitySO rankProbabilityData)
    {
        SummonableRanks = new List<CharRank>();
        summonableEntities = new Dictionary<CharRank, Entity[]>();
        summonProbability = new Dictionary<CharRank, float>();

        foreach (CharRank tempRank in Enum.GetValues(typeof(CharRank)))
        {
            if(tempRank == CharRank.none) continue;
            
            if (rankUnlockData.IsRankUnlocked(tempRank))
            {
                SummonableRanks.Add(tempRank);
            }
        }

        Entity[] entities;
        CharRank rank;
        for (int i = 0; i < SummonableRanks.Count; i++)
        {
            rank = SummonableRanks[i];
            entities = selectedCharacterData.CharListAsRankDictionary[rank].EntityList;
            summonableEntities.Add(rank, entities);
        }

        int count = 0;
        float[] probabilities = rankProbabilityData.Probabilities;
        foreach (CharRank tempRank in Enum.GetValues(typeof(CharRank)))
        {
            if(tempRank == CharRank.none) continue;
            
            if(SummonableRanks.Contains(tempRank))
            {
                summonProbability.Add(tempRank, probabilities[count]);
            }
            else
            {
                CharRank temp2 = tempRank - 1;
                while(SummonableRanks.Contains(temp2) == false)
                {
                    temp2--;
                }
                summonProbability[temp2] += probabilities[count];
            }
            count++;
        }
    }

    public Entity[] GetEntityByRank(CharRank rank) => summonableEntities[rank];
    public CharRank GetRankByProbability(List<CharRank> summonableRanks)
    {
        CharRank rank = CharRank.none;
        float randNum = UnityEngine.Random.Range(0, 100);
        float temp = 0;
        foreach (var item in summonProbability)
        {
            temp += item.Value;
            if (randNum <= temp && summonableRanks.Contains(item.Key))
            {
                rank = item.Key;

                break;
            }
        }

        if (rank == CharRank.none)
        {
            rank = summonableRanks[0];
        }

        return rank;
    }
}
