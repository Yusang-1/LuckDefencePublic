using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Manager
{
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private LobbyUIManager lobbyUI;
    [SerializeField] private BattleUIManager battleUI;
    [SerializeField] private LoadingUI loadingUI;    
    private GameObject currentActiveMainUI;
    private GameObject prevActiveMainUI;

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
        isStartCompleted = false;
        
        currentActiveMainUI = titleUI.gameObject;
        
        isStartCompleted = true;
    }

    public void ChangeMainUI()
    {        
        if(currentActiveMainUI != null)
        {
            prevActiveMainUI = currentActiveMainUI;
        }

        currentActiveMainUI = MainUIListBySceneIndex[SceneManager.GetActiveScene().buildIndex];

        currentActiveMainUI.SetActive(true);
    }
    
    public void DeActivePrevMainUI()
    {
        if (prevActiveMainUI != null)
        {
            prevActiveMainUI.SetActive(false);
        }
        loadingUI.gameObject.SetActive(false);
    }
}
