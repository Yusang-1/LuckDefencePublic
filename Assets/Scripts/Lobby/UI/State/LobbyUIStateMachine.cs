using System.Collections.Generic;
using System.Diagnostics;

public class LobbyUIStateMachine
{
    private ILobbyUIState currentState;
    private Stack<ILobbyUIState> stateStack;
    private GameManager gameManager;
    
    private ILobbyUIState lobbyState;
    private ILobbyUIState characterShopState;
    private ILobbyUIState manageCharacterState;

    public ILobbyUIState LobbyState => lobbyState;
    public ILobbyUIState CharacterShopState => characterShopState;
    public ILobbyUIState ManageCharacterState => manageCharacterState;

    public LobbyUIStateMachine(LobbyUIManager lobbyUIManager, GameManager gameManager)
    {
        this.gameManager = gameManager;
    
        lobbyState = lobbyUIManager.LowerUI;
        characterShopState = lobbyUIManager.CharacterShopUI;
        manageCharacterState = lobbyUIManager.ManagedCharacterUI;
    }

    public void Initialize(ILobbyUIState state)
    {
        stateStack = new Stack<ILobbyUIState>();

        currentState = state;
        state.ActiveUI();
    }

    public void ChangeState(ILobbyUIState state)
    {
        currentState.DeactiveUI();
        stateStack.Push(currentState);

        currentState = state;

        currentState.ActiveUI();

        if(currentState == lobbyState)
        {
            stateStack.Clear();

            gameManager.SaveLoad.SaveGame();
        }
    }

    public void UndoState()
    {
        if (currentState == lobbyState)
        {
            return;
        }

        currentState.DeactiveUI();

        currentState = stateStack.Pop();

        currentState.ActiveUI();
        
        if(currentState == lobbyState)
        {
            stateStack.Clear();

            gameManager.SaveLoad.SaveGame();
        }
    }

    public void ResetState()
    {
        if (currentState == lobbyState)
        {
            return;
        }

        ChangeState(lobbyState);

        stateStack.Clear();
        
        if(currentState == lobbyState)
        {
            stateStack.Clear();

            gameManager.SaveLoad.SaveGame();
        }
    }
}
