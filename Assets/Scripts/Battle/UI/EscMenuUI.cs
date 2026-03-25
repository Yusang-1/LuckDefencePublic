using System;
using UnityEngine;

public class EscMenuUI : MonoBehaviour
{
    public event Action RetryStage;

    private BattleSpeedController speedController; 

    private void Start()
    {
        if (speedController == null)
        {
            speedController = FindFirstObjectByType<BattleSpeedController>();
        }
    }

    private void OnEnable()
    {
        if (speedController == null)
        {
            speedController = FindFirstObjectByType<BattleSpeedController>();
        }

        speedController.ChangeGameSpeed(0);
    }

    private void OnDisable()
    {
        if (speedController == null)
        {
            speedController = FindFirstObjectByType<BattleSpeedController>();
        }

        speedController.ResumeGameSpeed();
    }

    public void OnGoToMainMenu()
    {
        gameObject.SetActive(false);

        SceneChanger.LoadSceneAsync("LobbyScene");
    }

    public void OnRetryStage()
    {
        gameObject.SetActive(false);

        RetryStage?.Invoke();
    }

    // 버튼에 할당
    public void OnOpenEscMenu()
    {
        gameObject.SetActive(true);
    }

    // 버튼에 할당
    public void OnCloseEscMenu()
    {
        gameObject.SetActive(false);
    }
}
