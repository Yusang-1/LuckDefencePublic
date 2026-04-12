using UnityEngine;

public class StageManager : Manager
{
    [SerializeField] private BattleDataSO battleData;

    private void Start()
    {
        isStartCompleted = true;
    }
    
    public void StartNextRound()
    {
        if(battleData.RoundNum == battleData.StageData.RoundCount - 1 || battleData.IsGameOver)
        {
            return;
        }

        battleData.RoundNum++;
    }
}
