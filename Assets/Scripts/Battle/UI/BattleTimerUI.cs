using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BattleTimerUI : UIPresenter
{   
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    
    private BattleTimer timer;

    private void Start()
    {
        if (timer == null)
        {
            timer = FindAnyObjectByType<BattleTimer>();
        }
    }    

    private void OnDestroy()
    {
        timer.TimeChanged -= OnUpdateUI;
    }

    public void Initialize()
    {
        if(timer == null)
        {
            timer = FindAnyObjectByType<BattleTimer>();
        }
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
