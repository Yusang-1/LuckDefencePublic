using TMPro;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerCoinText;

    [SerializeField] private PlayerResourcesSO playerResourcesData;

    private void Start()
    {
        OnUpdatePlayerResourcesDataUI();

        playerResourcesData.ResourcesChanged += OnUpdatePlayerResourcesDataUI;
    }

    private void OnDestroy()
    {
        playerResourcesData.ResourcesChanged -= OnUpdatePlayerResourcesDataUI;
    }

    public void OnUpdatePlayerResourcesDataUI()
    {
        playerNameText.text = playerResourcesData.PlayerName;
        playerLevelText.text = playerResourcesData.PlayerLevel.ToString();
        playerCoinText.text = playerResourcesData.PlayerCoin.ToString();
    }
}
