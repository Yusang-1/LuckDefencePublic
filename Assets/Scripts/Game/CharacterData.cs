using UnityEngine;
using System.Collections.Generic;

public class CharacterData : MonoBehaviour
{
    [Header("characterLists")]
    [SerializeField] private CharacterListDataSO characterListData;
    [SerializeField] private CharacterListDataSO ownedCharacterListData;
    [SerializeField] private CharacterListDataSO selectedCharacterListData;    

    [Header("Counts")]
    [SerializeField] private int commonCount;
    [SerializeField] private int rareCount;
    [SerializeField] private int epicCount;
    [SerializeField] private int uniqueCount;
    [SerializeField] private int legendaryCount;
    [SerializeField] private int allCount;
    private Dictionary<CharRank, int> rankCountByRank;

    [Header("Temp ColorCode")]
    [SerializeField] private string commonColorCode;
    [SerializeField] private string rareColorCode;
    [SerializeField] private string epicColorCode;
    [SerializeField] private string uniqueColorCode;
    [SerializeField] private string legendaryColorCode;
    private Dictionary<CharRank, string> colorCodeByRank;

    [Space]
    // 캐릭터 코드의 랭크별 단위 (100부터는 common, 200부터는 rare)
    [SerializeField] int codeUnit;

    public CharacterListDataSO CharacterListData => characterListData;
    public CharacterListDataSO OwnedCharacterListData => ownedCharacterListData;
    public CharacterListDataSO SelectedCharacterListData => selectedCharacterListData;

    public int AllCount => allCount;
    public Dictionary<CharRank, int> RankCountByRank => rankCountByRank;
    public Dictionary<CharRank, string> ColorCodeByRank => colorCodeByRank;

    // 씬에 인스턴스가 중복으로 생기는 것 방지
    private static bool hasInstance = false;

    void Awake()
    {
        if (hasInstance)
        {
            Destroy(gameObject);
        }
        else
        {
            hasInstance = true;

            DontDestroyOnLoad(gameObject);
        }
    }

    public void Initialize()
    {
        characterListData.Initialize();
        ownedCharacterListData.Initialize();
        selectedCharacterListData.Initialize();

        rankCountByRank = new Dictionary<CharRank, int>();
        rankCountByRank.Add(CharRank.common, commonCount);
        rankCountByRank.Add(CharRank.rare, rareCount);
        rankCountByRank.Add(CharRank.epic, epicCount);
        rankCountByRank.Add(CharRank.unique, uniqueCount);
        rankCountByRank.Add(CharRank.legendary, legendaryCount);

        colorCodeByRank = new Dictionary<CharRank, string>();
        colorCodeByRank.Add(CharRank.common, commonColorCode);
        colorCodeByRank.Add(CharRank.rare, rareColorCode);
        colorCodeByRank.Add(CharRank.epic, epicColorCode);
        colorCodeByRank.Add(CharRank.unique, uniqueColorCode);
        colorCodeByRank.Add(CharRank.legendary, legendaryColorCode);
    }

    /// <summary>
    /// OwnedCharacter List에 캐릭터를 추가
    /// </summary>
    /// <param name="entity"></param>
    public void AddOwnedCharacter(Entity entity)
    {
        ownedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].AddCharacter(entity);
    }

    /// <summary>
    /// OwnedCharacter List에서 캐릭터를 제거
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveOwnedCharacter(Entity entity)
    {
        ownedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].RemoveCharacter(entity.Data.Code);

        // SelectedCharacter List에 제거된 캐릭터가 있다면 제거
        if(selectedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].IsCodeExist(entity.Data.Code) == true)
        {
            RemoveSelectedCharacter(entity);
        }
    }

    /// <summary>
    /// SelectedCharacter List에 캐릭터를 추가
    /// </summary>
    /// <param name="entity"></param>
    public void AddSelectedCharacter(Entity entity)
    {
        if (ownedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].IsCodeExist(entity.Data.Code) == false)
        {
            Debug.LogWarning("소유하지 않은 캐릭터를 엔트리에 넣으려고 시도");
            return;
        }

        selectedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].AddCharacter(entity);
        selectedCharacterListData.IsDirty = true;
    }

    /// <summary>
    /// SelectedCharacter List에서 캐릭터를 제거
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveSelectedCharacter(Entity entity)
    {
        selectedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].RemoveCharacter(entity.Data.Code);

        selectedCharacterListData.IsDirty = true;
    }

    /// <summary>
    /// 캐틱터 코드를 받아 캐릭터 랭크를 반환
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public CharRank GetCharRankByCode(int code)
    {
        return (CharRank)(code / codeUnit - 1);
    }

    /// <summary>
    /// 전투에 필요한 SelectedCharacter List가 모두 채워졌는지 확인
    /// </summary>
    /// <returns></returns>
    public bool isSelectedCharacterFull()
    {
        int count = 0;
        foreach(var item in selectedCharacterListData.CharListAsRankDictionary)
        {
            if((item.Value as SelectedCharListAsRank).isSelectedCharacterFull() == true)
            {
                count++;
            }
        }

        if(count == selectedCharacterListData.CharListAsRankDictionary.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
