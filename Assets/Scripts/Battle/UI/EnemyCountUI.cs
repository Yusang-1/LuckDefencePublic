using UnityEngine;
using TMPro;

public class EnemyCountUI : UIPresenter
{
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI enemyLimitCountText;

    [SerializeField] private BattleDataSO battleData;
    int limitCount;

    private void OnDestroy()
    {
        battleData.EnemyCountChanged -= OnUpdateUI;
    }

    public void Initialize(int limitCount)
    {
        battleData.EnemyCountChanged += OnUpdateUI;

        this.limitCount = limitCount;
        enemyCountText.text = "0";
        enemyLimitCountText.text = limitCount.ToString();
    }

    public override void OnUpdateUI<T>(T item)
    {
        enemyCountText.text = item.ToString();
    }

    public void OnReset()
    {
        enemyCountText.text = "0";
        enemyLimitCountText.text = limitCount.ToString();
    }    
}
