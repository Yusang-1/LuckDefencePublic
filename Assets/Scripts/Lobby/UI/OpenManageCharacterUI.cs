using UnityEngine;

public class OpenManageCharacterUI : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    [SerializeField] private ManagedCharacterUI managedCharacterUI;

    public void OnOpenManageCharacter()
    {
        lobbyUIManager.OpenUIState(managedCharacterUI);
    }
}
