using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class SpeedButtonUI : UIPresenter<float>
{
    [SerializeField] private TextMeshProUGUI speedValueText;
    [SerializeField] private Image speedImage;

    [SerializeField] private Sprite[] speedSprites;

    private BattleSpeedController speedController;

    private void Start()
    {
        speedController = FindFirstObjectByType<BattleSpeedController>();
        speedController.GameSpeedChanged += OnUpdateUI;

    }

    private void OnDestroy()
    {
        speedController.GameSpeedChanged -= OnUpdateUI;
    }

    public override void OnUpdateUI(float item)
    {
        ChangeSpeedValueText(item);
        ChangeSpeedImage(item);
    }

    private void ChangeSpeedValueText(float speedValue)
    {
        speedValueText.text = speedValue.ToString();
    }

    private void ChangeSpeedImage(float speedValue)
    {
        int count = 0;
        foreach(float value in speedController.PossibleGameSpeed)
        {
            if(speedValue == value)
            {
                speedImage.sprite = speedSprites[count];
                break;
            }
            count++;
        }
    }

    // 버튼 할당
    public void OnChangeSpeed()
    {
        speedController.ChangeGameSpeed();
    }       
}
