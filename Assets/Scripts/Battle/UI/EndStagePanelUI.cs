using UnityEngine;
using System;

public class EndStagePanelUI : MonoBehaviour
{
    public event Action RetryStage;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject StageClearPanel;

    private void Start()
    {
        gameObject.SetActive(true);
        StageClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void OnGoToMainMenu()
    {
        OnDeactivePanel();

        SceneChanger.LoadSceneAsync("LobbyScene");
    }

    public void OnRetryStage()
    {
        OnDeactivePanel();

        RetryStage?.Invoke();
    }

    public void OnShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void OnShowStageClearPanel()
    {
        StageClearPanel.SetActive(true);
    }

    public void OnDeactivePanel()
    {
        StageClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
}
