using UnityEngine;
using System.Collections;

public class LobbyManager : Manager, IManagerSceneEntry
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    [SerializeField] private BattleDataSO battleData;
    
    public IEnumerator Initialize()
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        lobbyUIManager = Instantiate(lobbyUIManager, uiManager.transform);
        lobbyUIManager.transform.SetSiblingIndex(0);
        yield return lobbyUIManager.Initialize(StartBattle);
    }
    
    public void DestroyPrevUIAfterLoad()
    {
        Destroy(lobbyUIManager.gameObject);
    }
    
    private void StartBattle(StageSO stageData)
    {
        CharacterData characterData = FindAnyObjectByType<CharacterData>();
        if (characterData.isSelectedCharacterFull() == false)
        {
            lobbyUIManager.OpenUIState(lobbyUIManager.ManagedCharacterUI);
            return;
        }
        
        battleData.SetStageData(stageData);
        SceneChanger.LoadSceneAsync("BattleScene");
    }
}
