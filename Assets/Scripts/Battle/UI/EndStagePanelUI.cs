using UnityEngine;
using System;

public class EndStagePanelUI : MonoBehaviour
{
    public event Action RetryStage;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject StageClearPanel;
    [SerializeField] private RewardShower rewardShower;

    private void Start()
    {
        gameObject.SetActive(true);
        StageClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        
        rewardShower.OnCollectAllReward += OnShowStageClearPanel;
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

    private void OnShowStageClearPanel()
    {
        StageClearPanel.SetActive(true);
    }
    
    public void ShowRewardPanel(RewardData[] rewards)
    {
        rewardShower.ShowRewardPanel(rewards);
    }

    public void OnDeactivePanel()
    {
        StageClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        rewardShower.gameObject.SetActive(false);
    }
}
