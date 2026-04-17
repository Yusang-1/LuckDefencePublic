using System.Collections;
using UnityEngine;

public class GameManager : Manager
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private PlayerResourcesSO playerResources;
    public UIManager UIManager => uiManager;
    public PlayerResourcesSO PlayerResources => playerResources;
    public SaveLoad SaveLoad => saveLoad;

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
        Initialize();
    }
    
    public void Initialize()
    {
        saveLoad.LoadGame();
    }

    void OnDestroy()
    {
        saveLoad.SaveGame();
    }
    
    public void DeactivePrevUIAfterLoad()
    {
        uiManager.DeActivePrevMainUI();
    }
}
