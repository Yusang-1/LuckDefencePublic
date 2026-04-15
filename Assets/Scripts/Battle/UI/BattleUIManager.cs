using System.Collections;
using UnityEngine;

public class BattleUIManager : Manager
{
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private BattleTimerUI timerUI;
    [SerializeField] private ResourcesUI resourcesUI;
    [SerializeField] private EnemyCountUI enemyCountUI;
    [SerializeField] private SummonUI summonUI;
    [SerializeField] private StartStageButton startStageButton;
    [SerializeField] private EndStagePanelUI endStagePanelUI;
    [SerializeField] private EscMenuUI escMenuUI;
    [SerializeField] private SelectPlatformUIContainer selectPlatformContainer;

    [Space]
    [SerializeField] private BattleDataSO battleData;

    public EnemyCountUI EnemyCountUI => enemyCountUI;
    public StartStageButton StartStageButton => startStageButton;
    public EndStagePanelUI EndStagePanelUI => endStagePanelUI;
    public EscMenuUI EscMenuUI => escMenuUI;

    public IEnumerator Initialize(StageSO stageData, BattleTimer timer)
    {
        isStartCompleted = false;
        
        timerUI.Initialize(timer);
        resourcesUI.Initialize();
        EnemyCountUI.Initialize(stageData.MaxEnemyCount);
        summonUI.Initialize();
        
        yield return selectPlatformContainer.Initialize();        

        battleData.EnoughCoin += summonUI.EnableButton;
        battleData.NotEnoughCoin += summonUI.DisableButton;
        
        isStartCompleted = true;
    }

    private void OnDestroy()
    {
        battleData.EnoughCoin -= summonUI.EnableButton;
        battleData.NotEnoughCoin -= summonUI.DisableButton;
    }

    public void ResetBattleUI()
    {
        enemyCountUI.OnReset();
        endStagePanelUI.OnDeactivePanel();
        escMenuUI.gameObject.SetActive(false);
        startStageButton.OnOpenUI();
    }

    public void EnableBattleUI()
    {
        loadingUI.SetActive(false);
        battleUI.SetActive(true);
        startStageButton.Initialize();
    }
}
