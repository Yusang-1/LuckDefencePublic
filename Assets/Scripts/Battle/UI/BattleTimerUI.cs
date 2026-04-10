using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BattleTimerUI : UIPresenter
{   
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    
    private BattleTimer timer;  

    private void OnDestroy()
    {
        timer.TimeChanged -= OnUpdateUI;
    }

    public void Initialize(BattleTimer timer)
    {
        this.timer = timer;
        timer.TimeChanged += OnUpdateUI;
    }

    public override void OnUpdateUI<T>(T item)
    {
        ChangeTimerText(item);
    }

    private void ChangeTimerText<T>(T time) where T : IConvertible
    {  
        float floatTime = Convert.ToSingle(time);
        timerText.text = floatTime.ToString("N2");
    }
}
