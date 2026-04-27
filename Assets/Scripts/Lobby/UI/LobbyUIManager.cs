using UnityEngine;
using System;
using System.Collections;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LowerUI lowerUI;
    [SerializeField] private CharacterShopUI characterShopUI;
    [SerializeField] private ManagedCharacterUI managedCharacterUI;
    [SerializeField] private StageSelectUI stageSelectUI;

    private LobbyUIStateMachine stateMachine;

    public LowerUI LowerUI => lowerUI;
    public CharacterShopUI CharacterShopUI => characterShopUI;
    public ManagedCharacterUI ManagedCharacterUI => managedCharacterUI;
    public StageSelectUI StageSelectUI => stageSelectUI;

    public IEnumerator Initialize(Action<StageSO> battleStarter)
    {        
        CharacterData characterData = FindFirstObjectByType<CharacterData>();
        
        yield return StartCoroutine(characterShopUI.Initialize(characterData));

        yield return StartCoroutine(lowerUI.Initialize());

        yield return StartCoroutine(managedCharacterUI.Initialize(characterData));
        
        stageSelectUI.Initialize();
        stageSelectUI.StageSelected += battleStarter;

        stateMachine = new LobbyUIStateMachine(this, FindAnyObjectByType<GameManager>());
        stateMachine.Initialize(stateMachine.LobbyState);                 
    }

    public void OpenUIState<T>(T state) where T : ILobbyUIState
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
