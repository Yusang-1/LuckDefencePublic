using UnityEngine;
using TMPro;

public class ResourcesUI : UIPresenter
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

    public override void OnUpdateUI<T>(T item)
    {
        UpdateCoinUI(item);
    }

    private void UpdateCoinUI<T>(T item)
    {
        coinText.text = item.ToString();
    }
}
