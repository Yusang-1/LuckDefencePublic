using UnityEngine;
using System.Collections;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LowerUI lowerUI;
    [SerializeField] private CharacterShopUI characterShopUI;
    [SerializeField] private ManagedCharacterUI managedCharacterUI;

    private LobbyUIStateMachine stateMachine;

    public LowerUI LowerUI => lowerUI;
    public CharacterShopUI CharacterShopUI => characterShopUI;
    public ManagedCharacterUI ManagedCharacterUI => managedCharacterUI;

    private IEnumerator Start()
    {
        yield return StartCoroutine(characterShopUI.Initialize());

        yield return StartCoroutine(lowerUI.Initialize());

        yield return StartCoroutine(managedCharacterUI.Initialize());

        stateMachine = new LobbyUIStateMachine(this, FindAnyObjectByType<GameManager>());
        stateMachine.Initialize(stateMachine.LobbyState);
    }

    public void OpenUIState(ILobbyUIState state)
    {
        stateMachine.ChangeState(state);
    }

    public void OnBackButton()
    {
        stateMachine.UndoState();
    }

    public void OnHomeButton()
    {
        stateMachine.ResetState();
    }
}
