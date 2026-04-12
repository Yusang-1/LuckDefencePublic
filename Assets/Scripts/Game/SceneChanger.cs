using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private LoadingUI loadingUI;

    [SerializeField] private static GameManager gameManagerStatic;
    [SerializeField] private static LoadingUI loadingUIStatic;
    
    private static Manager[] managers;
    static AsyncOperation asyncOperation;

    private void Start()
    {
        instance = this;

        gameManagerStatic = gameManager;
        loadingUIStatic = loadingUI;
    }

    public static void LoadSceneAsync(string sceneName)
    {
        loadingUIStatic.gameObject.SetActive(true);

        instance.StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private static IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        asyncOperation.completed += OnLoadOperationCompleted;

        while (asyncOperation.isDone == false)
        {
            loadingUIStatic.LoadingBarUI.SetLoadingBar(asyncOperation.progress);
            
            if(asyncOperation.progress == 0.9f)
            {
                loadingUIStatic.LoadingCompleted();
                asyncOperation.allowSceneActivation = true;
                yield return WaitForStart();
            }
            yield return null;
        }
        
        loadingUIStatic.LoadingCompleted();
        OnLoadOperationCompleted();
        
        asyncOperation.completed -= OnLoadOperationCompleted;
        loadingUIStatic.gameObject.SetActive(false);
    }
    
    private static IEnumerator WaitForStart()
    {
        managers = FindObjectsByType<Manager>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        int count;
        while(true)
        {
            count = 0;
            foreach(var manager in managers)
            {
                if(manager.isStartCompleted) count++;                
            }
            yield return null;
            if(count == managers.Length) break;
        }
        
        while(!loadingUIStatic.IsPressScreen)
        {
            yield return null;
        }
    }

    private static void OnLoadOperationCompleted(AsyncOperation asyncOperation)
    {
        gameManagerStatic.Initialize();                        
    }
    private static void OnLoadOperationCompleted()
    {
        gameManagerStatic.Initialize();                        
    }
}
