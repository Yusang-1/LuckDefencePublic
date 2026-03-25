using UnityEngine;

public class Platform : MonoBehaviour, ISelectableObject, IHoldableObject
{
    [SerializeField] private Platforms platforms;
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private PlatformHoldSelector holdSelector;
    [SerializeField] private Promotion promotion;
    [SerializeField] private TargetSearcher targetSearcher;

    [SerializeField] private int currentEntityCode;
    [SerializeField] private int entityCount;
    [SerializeField] private CharRank rank;

    private Entity[] entities;
    [SerializeField] private Entity target;
    private Vector3 targetLastPosition;
    private const int maxAvailableEntityCount = 3;
    private int index;

    public int Index => index;
    public int EntityCount => entityCount;
    public CharRank Rank => rank;
    public Entity[] Entities => entities;
    public Entity Target
    {
        get => target;
        set
        {
            if (target == value)
            {
                return;
            }
            else if(target != null)
            {
                target.BattleData.EntityDied -= TargetIsUnavailable;
            }

            target = value;

            if(target != null)
            {
                target.BattleData.EntityDied += TargetIsUnavailable;
            }

            foreach (Character character in entities)
            {
                if(character != null)
                {
                    character.IsAttackable = target != null ? true : false;
                }
            }
        }
    }
    public Vector3 TargetLastPosition => targetLastPosition;

    public void Start()
    {
        promotion = FindFirstObjectByType<Promotion>();
        entities = new Entity[maxAvailableEntityCount];
        entityCount = 0;
        platformPosData.Initialize();
        rank = CharRank.none;
    }

    public void GetIndex(int index)
    {
        this.index = index;
    }

    /// <summary>
    /// 현제 플렛폼에 캐릭터가 얼마나 차있는지 확인후 알맞은 위치 반환
    /// </summary>
    /// <param name="rank"></param>
    /// <returns></returns>
    public Vector3 GetPosition(CharRank rank)
    {
        Vector3 pos = platformPosData.PlatformPosDict[rank].Pos[entityCount] + transform.position;

        return pos;
    }

    public void ResetPlatform()
    {
        entityCount = 0;
        currentEntityCode = 0;
        rank = CharRank.none;
        foreach (var entity in entities)
        {
            if(entity != null)
            {
                entity.gameObject.SetActive(false);
            }
        }

        platforms.DataChanged(index);
    }

    public void Migration()
    {
        entityCount = 0;
        currentEntityCode = 0;
        rank = CharRank.none;

        platforms.DataChanged(index);
    }

    public bool CheckEntityAvailable(int code)
    {        
        return (entityCount == 0 || (currentEntityCode == code && entityCount != maxAvailableEntityCount));
    }

    public bool CheckIsPromotionable()
    {
        if (entityCount == maxAvailableEntityCount && rank < CharRank.legendary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIsRankSummonable(CharRank rank)
    {
        if (this.rank == CharRank.none || this.rank == rank)
        {
            return true;
        }
        else
            return false;
    }

    public void EntitySpawned(GameObject spawnedObject)
    {
        Entity entity = spawnedObject.GetComponent<Entity>();        

        currentEntityCode = entity.Data.Code;
        entities[entityCount] = entity;

        CharacterSO charSO = entity.Data as CharacterSO;
        entityCount += charSO.Weight;

        rank = charSO.Rank;

        (entity as Character).EntityActivated();
        (entity as Character).GetPlatform(this);

        targetSearcher.Initialize(entity as Character);

        platforms.DataChanged(index);
    }

    public void Selected()
    {
        if (entities[0] == null)
        {
            platforms.SelectedPlatformIndex = -1;
            return;
        }

        bool value = CheckIsPromotionable();

        promotion.GetPlatformData(new PlatformData(index, rank), value);

        platforms.SelectedPlatformIndex = index;
    }

    public void SelectedEnd()
    {
        platforms.SelectedPlatformIndex = -1;
    }

    public void Holded()
    {
        holdSelector.Holded(index);
    }

    public void HoldReleased()
    {
        holdSelector.Released(index);
    }

    public void SetTarget(Entity entity)
    {
        if(entity == null && Target != null)
        {
            targetLastPosition = Target.transform.position;
        }

        Target = entity;
    }

    private void TargetIsUnavailable()
    {
        foreach (Character character in entities)
        {
            if (character != null)
            {
                character.IsAttackable = false;
            }
        }
        Target = null;
    }
}

public struct PlatformData
{
    private int index;
    private CharRank rank;

    public int Index => index;
    public CharRank Rank => rank;

    public PlatformData(int index, CharRank rank)
    {
        this.index = index; this.rank = rank;
    }
}