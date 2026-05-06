using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleTimerUI : UIPresenter<float>
{
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;

    private BattleTimer timer;

    private void OnDestroy()
    {
        if (timer != null)
            timer.TimeChanged -= OnUpdateUI;
    }

    public void Initialize(BattleTimer timer)
    {
        this.timer = timer;
        timer.TimeChanged += OnUpdateUI;
    }

    public override void OnUpdateUI(float item)
    {
        ChangeTimerText(item);
    }

    private void ChangeTimerText(float time)
    {
        char[] chars = CachedTextNumber.GetCachedText(time, out int length);
        timerText.SetCharArray(chars, 0, length);
    }
}
