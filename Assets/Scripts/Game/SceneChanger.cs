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

        while (asyncOperation.isDone == false)
        {
            loadingUIStatic.LoadingBarUI.SetLoadingBar(asyncOperation.progress);
            
            yield return null;
        }
        
        ActiveUIAfterLoad();
        yield return WaitForStart();
        loadingUIStatic.LoadingCompleted();
        DeactivePrevUIAfterLoad();
    }
    
    /// <summary>
    /// Manager클래스의 start완료를 확인하는 코루틴
    /// </summary>
    /// <returns></returns>
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
    }
    
    private static void ActiveUIAfterLoad()
    {
        gameManagerStatic.ActiveUIAfterLoad();
    }
    
    private static void DeactivePrevUIAfterLoad()
    {
        gameManagerStatic.DeactivePrevUIAfterLoad();
    }    
}
