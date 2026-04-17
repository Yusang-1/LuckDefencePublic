using UnityEngine;
using System.Collections;

public class LobbyManager : Manager, IManagerSceneEntry
{
    [SerializeField] private LobbyUIManager lobbyUIManager;    

    public IEnumerator Initialize()
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        lobbyUIManager = Instantiate(lobbyUIManager, uiManager.transform);
        lobbyUIManager.transform.SetSiblingIndex(0);
        yield return lobbyUIManager.Initialize();
    }
    
    public void DestroyPrevUIAfterLoad()
    {
        Destroy(lobbyUIManager.gameObject);
    }
}
