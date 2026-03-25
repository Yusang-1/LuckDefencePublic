using UnityEngine;

public class BattleUIManager : MonoBehaviour
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

    [Space]
    [SerializeField] private BattleDataSO battleData;

    public EnemyCountUI EnemyCountUI => enemyCountUI;
    public StartStageButton StartStageButton => startStageButton;
    public EndStagePanelUI EndStagePanelUI => endStagePanelUI;
    public EscMenuUI EscMenuUI => escMenuUI;

    public void Initialize(StageSO stageData)
    {
        timerUI.Initialize();
        resourcesUI.Initialize();
        EnemyCountUI.Initialize(stageData.MaxEnemyCount);
        summonUI.Initialize();
        startStageButton.Initialize();

        battleData.EnoughCoin += summonUI.EnableButton;
        battleData.NotEnoughCoin += summonUI.DisableButton;
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
    }
}
