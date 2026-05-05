using UnityEngine;

public class GameManager : Manager
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private PlayerResourcesSO playerResources;
    [SerializeField] private StagesSO stagesData;
    [SerializeField] private RankUnlockSO rankUnlockData;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private RewardHandler rewardHandler;
    
    public UIManager UIManager => uiManager;
    public PlayerResourcesSO PlayerResources => playerResources;
    public StagesSO StagesData => stagesData;
    public SaveLoad SaveLoad => saveLoad;
    public RewardHandler RewardHandler => rewardHandler;
    public CharacterData CharacterData => characterData;

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
    
    private void Start()
    {
        Initialize();        
    }
    
    void OnDestroy()
    {
        saveLoad.SaveGame();
        UnsubscribeRewardHandler();
    }
    
    public void Initialize()
    {
        saveLoad.LoadGame();
        SubscribeRewardHandler();
    }
    
    private void SubscribeRewardHandler()
    {
        rewardHandler.OnGetRewardCoin += playerResources.AddPlayerCoin; 
        rewardHandler.OnGetRewardUnlockStage += stagesData.StageUnlock;
        rewardHandler.OnGetRewardUnlockRank += rankUnlockData.UnlockRank;
        rewardHandler.OnGetRewardCharacter += characterData.AddOwnedCharacter;
    }
    
    private void UnsubscribeRewardHandler()
    {
        rewardHandler.OnGetRewardCoin -= playerResources.AddPlayerCoin;
        rewardHandler.OnGetRewardUnlockStage -= stagesData.StageUnlock;
        rewardHandler.OnGetRewardUnlockRank -= rankUnlockData.UnlockRank;
        rewardHandler.OnGetRewardCharacter -= characterData.AddOwnedCharacter;
    }
    
    public void DeactivePrevUIAfterLoad()
    {
        uiManager.DeActivePrevMainUI();
    }
}
