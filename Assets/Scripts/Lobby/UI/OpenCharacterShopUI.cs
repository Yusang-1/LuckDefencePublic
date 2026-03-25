using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCharacterShopUI : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    [SerializeField] private CharacterShopUI characterShopUI;

    public void OnOpenCharacterShop()
    {
        lobbyUIManager.OpenUIState(characterShopUI);
    }
}
