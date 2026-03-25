using UnityEngine;
using System;

public class BattleSpeedController : MonoBehaviour
{
    public event Action<float> GameSpeedChanged;

    [SerializeField] private float[] possibleGameSpeed;

    private int currentSpeedIndex;
    private int maxSpeedIndex;

    private int CurrentSpeedIndex
    {
        get => currentSpeedIndex;
        set
        {
            currentSpeedIndex = value;
            GameSpeedChanged?.Invoke(possibleGameSpeed[value]);
        }
    }

    public float[] PossibleGameSpeed => possibleGameSpeed;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        CurrentSpeedIndex = 0;
        maxSpeedIndex = possibleGameSpeed.Length - 1;
    }

    public void ChangeGameSpeed()
    {
        if (CurrentSpeedIndex == maxSpeedIndex)
        {
            CurrentSpeedIndex = 0;
        }
        else
        {
            CurrentSpeedIndex++;
        }

        Time.timeScale = possibleGameSpeed[CurrentSpeedIndex];        
    }

    public void ChangeGameSpeed(float speed)
    {
        Time.timeScale = speed;
        GameSpeedChanged?.Invoke(speed);
    }

    public void ResumeGameSpeed()
    {
        Time.timeScale = possibleGameSpeed[CurrentSpeedIndex];
        GameSpeedChanged?.Invoke(possibleGameSpeed[CurrentSpeedIndex]);
    }
}
