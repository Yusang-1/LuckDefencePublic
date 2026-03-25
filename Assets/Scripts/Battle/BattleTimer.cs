using System;
using UnityEngine;

public class BattleTimer : MonoBehaviour
{
    public event Action TimeIsOver;
    public event Action<float> TimeChanged;

    [SerializeField] private float minTimeUnit;
    [SerializeField] private float warningTime;
    //[SerializeField] private float maxTime;

    private float currentTime;
    private bool isPlaying;

    protected float CurrentTime
    {
        get => currentTime;
        set
        {
            currentTime = Mathf.Clamp(value, 0, value);
            TimeChanged?.Invoke(currentTime);

            if (currentTime == 0 && isPlaying)
            {
                TimeIsOver?.Invoke();
            }
        }
    }

    private float tempTime;
    private int timeCount;
    private void Update()
    {
        if (CurrentTime == 0 || isPlaying == false)
        {
            return;
        }

        tempTime += Time.deltaTime;

        if (tempTime >= minTimeUnit)
        {
            timeCount = 0;
            while (tempTime > minTimeUnit)
            {
                tempTime -= minTimeUnit;
                timeCount++;
            }

            CurrentTime -= minTimeUnit * timeCount;
        }
    }

    public void Initialize()
    {
        CurrentTime = 0;
    }

    public void OnStartTimerAddTime(RoundData data)
    {
        isPlaying = true;

        CurrentTime += data.AdditionalTime;
    }

    public void OnResetTimer()
    {
        isPlaying = false;

        CurrentTime = 0;
    }
}
