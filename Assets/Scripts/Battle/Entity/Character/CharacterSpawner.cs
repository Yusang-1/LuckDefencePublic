using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{    
    [SerializeField] private RankProbabilitySO probabilityData;
    [SerializeField] private BattleDataSO battleData;
    private CharacterFactoryContainer factoryContainer;
    private AbstractFactory[] factories => factoryContainer.Factories;
    private BattleMap battleMap;
    private Platforms platforms => battleMap.Platforms;

    private CharacterListDataSO charListData;

    private List<CharRank> summonableRanks;
    private List<int> summonableCharacterCodes;
    private List<int> availablePlatformIndex;

    private Dictionary<CharRank, AbstractFactory> factoryDict;

    public Dictionary<CharRank, AbstractFactory> FactoryDict => factoryDict;

    public IEnumerator Initialize(CharacterListDataSO characterListData, BattleMap battleMap, CharacterFactoryContainer factoryContainer)
    {
        charListData = characterListData;
        this.battleMap = battleMap;
        this.factoryContainer = factoryContainer;
        factoryDict = new Dictionary<CharRank, AbstractFactory>();
        summonableRanks = new List<CharRank>();
        summonableCharacterCodes = new List<int>();
        availablePlatformIndex = new List<int>();

        AbstractFactory factory;
        FactoryChar fc;
        for (int i = 0; i < factories.Length; i++)
        {
            factory = factories[i];
            fc = factory as FactoryChar;
            factory.Initialize(charListData.CharListAsRankDictionary[fc.Rank].EntityAsCodeDict);

            factoryDict.Add(fc.Rank, factory);

            yield return null;
        }
        probabilityData.Initialize();

        foreach(FactoryChar factor in  factories)
        {
            foreach(var item in factor.PooledEntityDict)
            {
                item.Value.CharacterSpawned += OnCharacterSpawned;
            }
        }
    }

    public void OnDisable()
    {
        foreach (FactoryChar factor in factories)
        {
            foreach (var item in factor.PooledEntityDict)
            {
                item.Value.CharacterSpawned -= OnCharacterSpawned;
            }
        }
    }

    public void OnCharacterSpawned(int platformIndex, GameObject go)
    {
        ResetData();
        platforms.PlatformList[platformIndex].EntitySpawned(go);
    }

    public void ResetData()
    {
        summonableRanks.Clear();
        summonableCharacterCodes.Clear();
        availablePlatformIndex.Clear();
    }

    public void SpawnEntity()
    {
        if(battleData.CurrentCoin < battleData.SpawnCost)
        {
            return;
        }

        battleData.CurrentCoin -= battleData.SpawnCost;

        CharRank rank = CheckSummonableRank();
        int charCode = CheckSummonableCharacterInRank(rank);
        int platformIndex = CheckAvailablePlatformIndexByCharacter(charCode);
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
            for (int i = 0; i < (int)CharRank.legendary; i++)
            {
                if (platform.CheckIsRankSummonable((CharRank)i) && !summonableRanks.Contains((CharRank)i))
                {
                    summonableRanks.Add((CharRank)i);
                }
            }

            if (summonableRanks.Count == (int)CharRank.legendary)
            {
                break;
            }
        }

        // 확률 기반으로 뽑을 랭크 결정
        CharRank rank = CharRank.none;
        float randNum = Random.Range(0, 100);
        float temp = 0;
        foreach (var item in probabilityData.ProbabilityDict)
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

    public int CheckSummonableCharacterInRank(CharRank rank)
    {
        int length = charListData.CharListAsRankDictionary[rank].EntityList.Length;
        int code;
        bool isAvailable;

        // 소환할 수 있는 캐릭터를 판별
        foreach (Platform platform in platforms.PlatformList)
        {
            for (int i = 0; i < length; i++)
            {
                code = charListData.CharListAsRankDictionary[rank].EntityList[i].Data.Code;
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

    public int CheckAvailablePlatformIndexByCharacter(int code)
    {
        bool isAvailable;

        // 해당 캐릭터가 들어갈 수 있는 플렛폼을 판별
        foreach (Platform platform in platforms.PlatformList)
        {
            isAvailable = platform.CheckEntityAvailable(code);

            if (isAvailable && !availablePlatformIndex.Contains(platform.Index))
            {
                availablePlatformIndex.Add(platform.Index);
            }
        }

        int randNum = Random.Range(0, availablePlatformIndex.Count);

        return availablePlatformIndex[randNum];
    }

    public Vector3 GetSummonPosition(int index, CharRank rank)
    {
        return platforms.PlatformList[index].GetPosition(rank);
    }

    public void PromotionEntity(PlatformData data)
    {
        platforms.PlatformList[data.Index].ResetPlatform();

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
