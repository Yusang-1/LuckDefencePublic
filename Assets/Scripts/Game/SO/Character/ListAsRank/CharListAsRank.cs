using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharList/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] protected CharRank rank;

    [SerializeField] protected Entity[] entityList;
    [SerializeField] protected int fullCount;
    [SerializeField] protected int[] codeList;
    [SerializeField] protected Entity[] defaultEntityList;

    protected Dictionary<int, Entity> entityAsCodeDict;
    protected bool isDirty;

    public CharRank Rank => rank;
    public Entity[] EntityList => entityList;
    public int[] CodeList => codeList;
    public Dictionary<int, Entity> EntityAsCodeDict => entityAsCodeDict;

    public bool IsDirty => isDirty;

    public virtual void Initialize()
    {
        isDirty = false;

        codeList = new int[fullCount];
        entityAsCodeDict = new Dictionary<int, Entity>();

        for (int i = 0; i < entityList.Length; i++)
        {
            if (entityList[i] == null)
            {
                continue;
            }

            int code = entityList[i].Data.Code;
            codeList[i] = code;
            entityAsCodeDict.Add(codeList[i], entityList[i]);
        }
    }

    public virtual void AddCharacter(Entity entity)
    {
        if (IsCodeExist(entity.Data.Code) == true)
        {
            Debug.LogWarning("이미 존재하는 코드를 추가");
            return;
        }

        int emptyIndex = 0;
        for (int i = 0; i < codeList.Length; i++)
        {
            //비어있는 자리를 찾으면 코드 저장 후 정렬
            if (codeList[i] == 0)
            {
                emptyIndex = i;
                codeList[i] = entity.Data.Code;
                Array.Sort<int>(codeList, 0, i + 1);

                break;
            }
        }

        entityList[emptyIndex] = entity;
        entityAsCodeDict.Add(entity.Data.Code, entity);

        // codeList에 맞춰 entityList 정렬 추후 더 효율적인 방법으로 정렬법 변경
        for (int i = 0; i < codeList.Length; i++)
        {
            if (codeList[i] == 0)
            {
                continue;
            }

            entityList[i] = entityAsCodeDict[codeList[i]];
        }

        isDirty = true;
    }

    public virtual void RemoveCharacter(int code)
    {
        if (IsCodeExist(code) == false)
        {
            Debug.LogWarning("존재하지 않는 코드를 제거");
            return;
        }

        int codeIndex = Array.IndexOf<int>(codeList, code);
        for (int i = 0; i < codeList.Length; i++)
        {
            //채워져있는 마지막 인덱스 (i - 1)을 찾아 지워야할 인덱스 (codeIndex)와 교환 후 삭제, 정렬
            if (codeList[i] == 0)
            {
                if (i - 1 == codeIndex)
                {
                    codeList[i - 1] = 0;
                    entityList[i - 1] = null;
                    Array.Sort<int>(codeList, 0, i - 1);
                }
                else
                {
                    codeList[codeIndex] = codeList[i - 1];
                    codeList[i - 1] = 0;
                    Array.Sort<int>(codeList, 0, i - 1);

                    entityList[codeIndex] = entityList[i - 1];
                    entityList[i - 1] = null;
                }
                break;
            }
        }

        entityAsCodeDict.Remove(code);

        for (int i = 0; i < codeList.Length; i++)
        {
            if (codeList[i] == 0)
            {
                continue;
            }

            entityList[i] = entityAsCodeDict[codeList[i]];
        }

        isDirty = true;
    }

    public bool IsCodeExist(int code)
    {
        return entityAsCodeDict.ContainsKey(code);
    }

    public void SetDirty(bool value)
    {
        isDirty = value;
    }
}
