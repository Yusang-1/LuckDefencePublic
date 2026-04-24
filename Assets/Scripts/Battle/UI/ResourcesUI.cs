using UnityEngine;
using TMPro;

public class ResourcesUI : UIPresenter<int>
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI jewlText;

    [SerializeField] private BattleDataSO battleData;
    
    private CachedTextNumber cachedTextNumber;
    
    private void OnDestroy()
    {
        battleData.CoinChanged -= OnUpdateUI;
    }

    public void Initialize()
    {
        battleData.CoinChanged += OnUpdateUI;
        cachedTextNumber = new CachedTextNumber();
    }

    public override void OnUpdateUI(int item)
    {
        UpdateCoinUI(item);
    }

    private void UpdateCoinUI(int item)
    {
        coinText.SetCharArray(cachedTextNumber.GetCachedText(item, out int length), 0, length);
    }
}
