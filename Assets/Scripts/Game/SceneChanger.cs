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

            if(asyncOperation.progress == 0.9f && loadingUIStatic.IsPressScreen == false)
            {
                loadingUIStatic.LoadingCompleted();                
            }

            if (loadingUIStatic.IsPressScreen == true)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        asyncOperation.completed -= OnLoadOperationCompleted;
        loadingUIStatic.gameObject.SetActive(false);
    }

    private static void OnLoadOperationCompleted(AsyncOperation asyncOperation)
    {
        gameManagerStatic.Initialize();
    }
}
