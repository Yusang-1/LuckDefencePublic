using UnityEngine;
using System.Collections;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LowerUI lowerUI;
    [SerializeField] private CharacterShopUI characterShopUI;
    [SerializeField] private ManagedCharacterUI managedCharacterUI;
    [SerializeField] private GameStartUI gameStartUI;

    private LobbyUIStateMachine stateMachine;

    public LowerUI LowerUI => lowerUI;
    public CharacterShopUI CharacterShopUI => characterShopUI;
    public ManagedCharacterUI ManagedCharacterUI => managedCharacterUI;

    public IEnumerator Initialize()
    {        
        CharacterData characterData = FindFirstObjectByType<CharacterData>();
        
        yield return StartCoroutine(characterShopUI.Initialize(characterData));

        yield return StartCoroutine(lowerUI.Initialize());

        yield return StartCoroutine(managedCharacterUI.Initialize(characterData));

        stateMachine = new LobbyUIStateMachine(this, FindAnyObjectByType<GameManager>());
        stateMachine.Initialize(stateMachine.LobbyState);
        
        gameStartUI.Initialize(characterData);                
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
