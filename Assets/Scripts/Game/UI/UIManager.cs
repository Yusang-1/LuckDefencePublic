using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUI;
    [SerializeField] private BattleUIManager battleUI;
    [SerializeField] private LoadingUI loadingUI;    
    [SerializeField] private GameObject currentActiveMainUI;

    [SerializeField] private GameObject[] MainUIListBySceneIndex;

    private static bool hasInstance = false;

    void Awake()
    {
        if (hasInstance)
        {
            Destroy(gameObject);
        }
        else
        {
            hasInstance = true;
            
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentActiveMainUI = lobbyUI.gameObject;
    }

    public void ChangeMainUI()
    {
        loadingUI.gameObject.SetActive(false);
        if(currentActiveMainUI != null)
        {
            currentActiveMainUI.SetActive(false);
        }

        currentActiveMainUI = MainUIListBySceneIndex[SceneManager.GetActiveScene().buildIndex];

        currentActiveMainUI.SetActive(true);
    }
}
