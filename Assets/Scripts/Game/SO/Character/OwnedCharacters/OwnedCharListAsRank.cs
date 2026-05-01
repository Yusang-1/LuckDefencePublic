using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "OwnedCharListAsRank", menuName = "Scriptable Objects/CharList/OwnedCharListAsRank")]
public class OwnedCharListAsRank : CharListAsRank, ISaveData
{
    

    public IDataStructForSave GetSaveData()
    {
        CharListAsRankSaveData saveData = new CharListAsRankSaveData
        {
            rank = (int)rank,
            codeList = codeList.ToArray()
        };

        return saveData;
    }

    public void SetLoadData(IDataStructForSave saveData)
    {
        Array.Clear(codeList, 0, codeList.Length);
        Array.Clear(entityList, 0, entityList.Length);
        entityAsCodeDict.Clear();

        CharListAsRankSaveData charSaveData = (CharListAsRankSaveData)saveData;
        rank = (CharRank)charSaveData.rank;
        codeList = charSaveData.codeList;
        fullCount = codeList.Length;

        //entityList와 entityAsCodeDict는 codeList를 기반으로 초기화        
        CharacterListDataSO characterListData = FindAnyObjectByType<CharacterData>().CharacterListData;
        Entity entity;
        for (int i = 0; i < codeList.Length; i++)
        {
            if (codeList[i] == 0)
            {
                continue;
            }

            if (characterListData.CharListAsRankDictionary[rank].EntityAsCodeDict.ContainsKey(codeList[i]))
            {
                entity = characterListData.CharListAsRankDictionary[rank].EntityAsCodeDict[codeList[i]];

                entityList[i] = entity;
                entityAsCodeDict.Add(codeList[i], entity);
            }
        }
    }

    public void SetDefaultData()
    {
        Array.Clear(codeList, 0, codeList.Length);
        Array.Clear(entityList, 0, entityList.Length);
        entityAsCodeDict.Clear();

        for (int i = 0; i < entityList.Length; i++)
        {
            if (i >= defaultEntityList.Length || defaultEntityList[i] == null)
            {
                continue;
            }

            AddCharacter(defaultEntityList[i]);
        }
    }

    public struct CharListAsRankSaveData : IDataStructForSave
    {
        public int rank;
        public int[] codeList;
    }
}
