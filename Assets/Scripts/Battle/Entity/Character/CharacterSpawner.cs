using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private BattleDataSO battleData;
    private CharacterFactoryContainer factoryContainer;
    private AbstractFactory[] factories => factoryContainer.Factories;
    private BattleMap battleMap;
    private Platforms platforms => battleMap.Platforms;

    private CharacterListDataSO charListData;

    private List<CharRank> currentSummonableRanks;
    private List<int> summonableCharacterCodes;

    private Dictionary<CharRank, AbstractFactory> factoryDict;
    
    private SummonInfo summonInfo;

    public IEnumerator Initialize(CharacterListDataSO characterListData, BattleMap battleMap, CharacterFactoryContainer factoryContainer, SummonInfo summonInfo)
    {
        charListData = characterListData;
        this.battleMap = battleMap;
        this.factoryContainer = factoryContainer;
        factoryDict = new Dictionary<CharRank, AbstractFactory>();
        currentSummonableRanks = new List<CharRank>();
        summonableCharacterCodes = new List<int>();
        this.summonInfo = summonInfo;
        
        AbstractFactory factory;
        FactoryChar fc;
        for (int i = 0; i < factories.Length; i++)
        {
            factory = factories[i];
            fc = factory as FactoryChar;
            if (summonInfo.SummonableRanks.Contains(fc.Rank))
            {
                factory.Initialize(charListData.CharListAsRankDictionary[fc.Rank].EntityAsCodeDict);
                factoryDict.Add(fc.Rank, factory);
            }
            yield return null;
        }

        foreach (FactoryChar factor in factories)
        {
            if(factor.PooledEntityDict == null) continue;
            foreach (var item in factor.PooledEntityDict)
            {
                item.Value.CharacterSpawned += OnCharacterSpawned;
            }
        }
    }

    public void OnDisable()
    {
        foreach (FactoryChar factor in factories)
        {
            if(factor.PooledEntityDict == null) continue;
            
            foreach (var item in factor.PooledEntityDict)
            {
                item.Value.CharacterSpawned -= OnCharacterSpawned;
            }
        }
    }

    public void OnCharacterSpawned(int platformIndex, GameObject go)
    {
        ResetData();
        platforms.EntitySpawned(platformIndex, go);
    }

    public void ResetData()
    {
        currentSummonableRanks.Clear();
        summonableCharacterCodes.Clear();
    }

    public void SpawnEntity()
    {
        if (battleData.CurrentCoin < battleData.SpawnCost)
        {
            return;
        }

        battleData.CurrentCoin -= battleData.SpawnCost;

        CharRank rank = CheckSummonableRank();
        int charCode = CheckSummonableCharacterInRank(rank);
        int platformIndex = platforms.CheckAvailablePlatformIndexByCharacter(charCode);
        Vector3 position = GetSummonPosition(platformIndex, rank);

        SummonData data = new((int)rank, charCode, platformIndex, position);

        OrderToFactory(data);
    }

    public void OrderToFactory(SummonData data)
    {
        factoryDict[(CharRank)data.CharRank].ActiveEntity(data);
    }

    public CharRank CheckSummonableRank()
    {
        // 플렛폼들을 순회하며 소환 가능한 랭크의 리스트를 만듦
        foreach (Platform platform in platforms.PlatformList)
        {
            foreach (CharRank tempRank in summonInfo.SummonableRanks)
            {
                if(tempRank == CharRank.none) continue;
                
                if (platform.CheckIsRankSummonable(tempRank) && !currentSummonableRanks.Contains(tempRank))
                {
                    currentSummonableRanks.Add(tempRank);
                }
            }

            if (currentSummonableRanks.Count == (int)CharRank.legendary)
            {
                break;
            }
        }

        return summonInfo.GetRankByProbability(currentSummonableRanks);
    }

    public int CheckSummonableCharacterInRank(CharRank rank)
    {
        Entity[] entities = summonInfo.GetEntityByRank(rank);
        int length = entities.Length;
        int code;
        bool isAvailable;

        // 소환할 수 있는 캐릭터를 판별
        foreach (Platform platform in platforms.PlatformList)
        {
            for (int i = 0; i < length; i++)
            {
                code = entities[i].Data.Code;
                isAvailable = platform.CheckEntityAvailable(code);

                if (isAvailable && !summonableCharacterCodes.Contains(code))
                {
                    summonableCharacterCodes.Add(code);
                }
            }

            // 해당 랭크에 모든 캐릭터가 소환 가능하면 탈출
            if (summonableCharacterCodes.Count == length)
            {
                break;
            }
        }

        int randNum = Random.Range(0, summonableCharacterCodes.Count);

        return summonableCharacterCodes[randNum];
    }

    public Vector3 GetSummonPosition(int index, CharRank rank)
    {
        return platforms.GetSummonPosition(index, rank);
    }

    public void PromotionEntity(PlatformData data)
    {
        platforms.ResetPlatform(data.Index);
        CharRank rank = data.Rank + 1;
        int charCode = CheckSummonableCharacterInRank(rank);
        int platformIndex = data.Index;
        Vector3 position = GetSummonPosition(platformIndex, rank);

        SummonData summonData = new((int)rank, charCode, platformIndex, position);

        factoryDict[(CharRank)summonData.CharRank].ActiveEntity(summonData);
    }
}

public struct SummonData
{
    private readonly int charRank;
    private readonly int charCode;
    private readonly int platformIndex;
    private Vector3 position;

    public SummonData(int charRank, int charCode, int platformIndex, Vector3 position)
    {
        this.charRank = charRank;
        this.charCode = charCode;
        this.platformIndex = platformIndex;
        this.position = position;
    }

    public readonly int CharRank => charRank;
    public readonly int CharCode => charCode;
    public readonly int PlatformIndex => platformIndex;
    public readonly Vector3 Position => position;
}
