using UnityEngine;

public class OpenSelectStageUI : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    [SerializeField] private StageSelectUI stageSelectUI;

    public void OnOpenStageSelect()
    {
        lobbyUIManager.OpenUIState(stageSelectUI);
    }
}
