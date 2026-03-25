using UnityEngine;

public class StartStageButton : MonoBehaviour
{
    private BattleManager battleManager;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void OnStartStage()
    {
        if(battleManager == null)
        {
            battleManager = FindFirstObjectByType<BattleManager>();
        }
        battleManager.StartBattle();

        gameObject.SetActive(false);
    }

    public void OnOpenUI()
    {
        gameObject.SetActive(true);
    }
}
