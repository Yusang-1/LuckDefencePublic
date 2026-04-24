using UnityEngine;
using System.Collections;

public class BattleManager : Manager, IManagerSceneEntry
{
    [Header("Managers")]
    [SerializeField] private StageManager stageManager;
    [SerializeField] private BattleUIManager battleUIManager; //instantiate in runtime

    [Header("Spawners")]
    [SerializeField] private CharacterSpawner characterSpawner; //instantiate in runtime
    [SerializeField] private EnemySpawner enemySpawner; //instantiate in runtime
    private HPSpawner hpSpawner => battleUIManager.HpSpawner;

    [Header("Datas")]
    [SerializeField] private StageSO stageData;
    [SerializeField] private BattleDataSO battleData;
    [SerializeField] private CharacterListDataSO charListData;
    [SerializeField] private EnemyList enemyList; //getComponent in runtime in enemySpawner

    [Space]
    [SerializeField] private BattleMap battleMap; //instantiate in runtime
    private Platforms platforms => battleMap.Platforms;
    [SerializeField] private BattleSpeedController speedController;
    [SerializeField] private BattleTimer battleTimer;
    [SerializeField] private CharacterFactoryContainer characterFactoryContainer; //instantiate in runtime
    
    public IEnumerator Initialize()
    {
        battleData.Initialize(stageData);
        speedController.Initialize();        
        battleTimer = new BattleTimer();
        
        battleUIManager = Instantiate(battleUIManager, FindAnyObjectByType<UIManager>().transform);
        battleUIManager.transform.SetSiblingIndex(0);
        characterFactoryContainer = Instantiate(characterFactoryContainer);
        battleMap = Instantiate(battleMap);
        battleMap.BeaconContainer.Initialize();
        hpSpawner.Initialize(stageData);
        characterSpawner = Instantiate(characterSpawner);
        enemySpawner = Instantiate(enemySpawner);        
        enemySpawner.Initialize(stageData.RoundData, hpSpawner, BeaconContainer.s_Beacons[0]);
        enemyList = enemySpawner.GetComponent<EnemyList>();
        
        yield return battleUIManager.Initialize(stageData, battleTimer);

        battleData.StartNextRound += enemySpawner.SpawnEnemy;
        battleData.StartNextRound += battleTimer.OnStartTimerAddTime;

        battleData.EnemyFull += battleUIManager.EndStagePanelUI.OnShowGameOverPanel;
        battleData.EnemyFull += OnResetBattle;        

        battleData.AllEnemyDied += ClearBattle;

        battleTimer.TimeIsOver += stageManager.StartNextRound;

        battleUIManager.EndStagePanelUI.RetryStage += OnRestartBattle;
        battleUIManager.EscMenuUI.RetryStage += OnRestartBattle;

        yield return characterSpawner.Initialize(charListData, battleMap, characterFactoryContainer);

        battleUIManager.EnableBattleUI();        
    }

    private void OnDestroy()
    {
        battleData.StartNextRound -= enemySpawner.SpawnEnemy;
        battleData.StartNextRound -= battleTimer.OnStartTimerAddTime;
        battleData.EnemyFull -= battleUIManager.EndStagePanelUI.OnShowGameOverPanel;
        battleData.EnemyFull -= enemySpawner.OnStopActiveCoroutine;
        battleData.EnemyFull -= OnResetBattle;
        battleData.AllEnemyDied -= ClearBattle;
        battleTimer.TimeIsOver -= stageManager.StartNextRound;
        battleUIManager.EndStagePanelUI.RetryStage -= OnRestartBattle;
        battleUIManager.EscMenuUI.RetryStage -= OnRestartBattle;
    }

    public void StartBattle()
    {
        stageManager.StartNextRound();
        battleData.OnResetData();
    }
    
    private void ClearBattle()
    {
        FindAnyObjectByType<GameManager>().PlayerResources.AddReward(stageData.RewardData);
        battleUIManager.EndStagePanelUI.OnShowStageClearPanel();
        foreach (var platform in platforms.PlatformList)
        {
            platform.ResetPlatform();
        }
    }
    
    private void OnResetBattle()
    {
        battleTimer.OnResetTimer();
        enemySpawner.OnStopActiveCoroutine();
        hpSpawner.OnDeactiveAllHP();
        foreach (var platform in platforms.PlatformList)
        {
            platform.ResetPlatform();
        }
        GameOver();
    }
    private void OnRestartBattle()
    {
        OnResetBattle();

        enemyList.OnDeactivateAllEnemy();        
        battleData.OnResetData();
        speedController.Initialize();
        battleUIManager.ResetBattleUI();
    }

    private void GameOver()
    {
        battleData.IsGameOver = true;
    }

    public void DestroyPrevUIAfterLoad()
    {
        Destroy(battleUIManager.gameObject);
    }
}
