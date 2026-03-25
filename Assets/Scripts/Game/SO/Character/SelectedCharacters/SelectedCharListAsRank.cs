using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "SelectedCharListAsRank", menuName = "Scriptable Objects/SelectedCharListAsRank")]
public class SelectedCharListAsRank : CharListAsRank, ISaveData
{
    // 숫자가 높은 것부터 대체
    [SerializeField] private int[] changePriority;

    public override void Initialize()
    {
        base.Initialize();

        changePriority = new int[fullCount];
    }

    public override void AddCharacter(Entity entity)
    {
        if (IsCodeExist(entity.Data.Code) == true)
        {
            Debug.LogWarning("이미 존재하는 코드를 추가");
            return;
        }

        int listFilled = 0;
        for(int i = 0; i < entityList.Length; i++)
        {
            if (entityList[i] != null)
            {
                listFilled++;
            }
        }

        // 해당 랭크의 티오가 다 찼을 경우 가장 오래전에 추가됐던 캐릭터를 대체
        int index;
        if(listFilled == fullCount)
        {
            index = Array.IndexOf<int>(changePriority, changePriority.Max<int>());

            entityAsCodeDict.Remove(codeList[index]);

            codeList[index] = entity.Data.Code;
            entityList[index] = entity;
            entityAsCodeDict.Add(entity.Data.Code, entity);
        }
        else
        {
            int emptyIndex = 0;
            for (int i = 0; i < codeList.Length; i++)
            {
                //비어있는 자리를 찾으면 코드 저장
                if (codeList[i] == 0)
                {
                    emptyIndex = i;
                    codeList[i] = entity.Data.Code;

                    break;
                }
            }

            entityList[emptyIndex] = entity;
            entityAsCodeDict.Add(entity.Data.Code, entity);

            index = Array.IndexOf<int>(codeList, entity.Data.Code);
        }
            
        // 새로 추가된 index의 priority는 1로 나머지 priority는 1을 추가        
        changePriority[index] = 1;
        for (int i = 0; i < changePriority.Length; i++)
        {
            if(i == index || changePriority[i] == 0)
            {
                continue;
            }

            changePriority[i]++;
        }
        
        isDirty = true;
    }

    public override void RemoveCharacter(int code)
    {
        if (IsCodeExist(code) == false)
        {
            Debug.LogWarning("존재하지 않는 코드를 제거");
            return;
        }        

        int codeIndex = Array.IndexOf<int>(codeList, code);
        changePriority[codeIndex] = 0;

        entityAsCodeDict.Remove(codeList[codeIndex]);
        codeList[codeIndex] = 0;
        entityList[codeIndex] = null;        

        isDirty = true;
    }

    public bool isSelectedCharacterFull()
    {
        if (entityList.Contains(null))
        {
            return false;
        }       
        else
        {
            return true;
        }
    }
    
    public override void SetLoadData(IDataStructForSave saveData)
    {
        Array.Clear(codeList, 0, codeList.Length);
        Array.Clear(entityList, 0, entityList.Length);
        entityAsCodeDict.Clear();
        
        CharListAsRankSaveData charSaveData = (CharListAsRankSaveData)saveData;
        rank = (CharRank)charSaveData.rank;
        codeList = charSaveData.codeList;
        fullCount = codeList.Length;
        
        //entityList와 entityAsCodeDict는 codeList를 기반으로 초기화        
        CharacterListDataSO characterListData = FindAnyObjectByType<CharacterData>().OwnedCharacterListData;
        Entity entity;
        for(int i = 0; i < codeList.Length; i++)
        {
            if(codeList[i] == 0)
            {
                continue;
            }
            entity = characterListData.CharListAsRankDictionary[rank].EntityAsCodeDict[codeList[i]];
            entityList[i] = entity;
            entityAsCodeDict.Add(codeList[i], entity);
        }
    }
    
    
}
