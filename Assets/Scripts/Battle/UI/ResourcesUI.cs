using UnityEngine;
using TMPro;

public class ResourcesUI : UIPresenter<int>
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI jewlText;

    [SerializeField] private BattleDataSO battleData;

    private void OnDestroy()
    {
        battleData.CoinChanged -= OnUpdateUI;
    }

    public void Initialize()
    {
        battleData.CoinChanged += OnUpdateUI;
    }

    public override void OnUpdateUI(int item)
    {
        UpdateCoinUI(item);
    }

    private void UpdateCoinUI(int item)
    {
        coinText.text = item.ToString();
    }
}
