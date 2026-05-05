using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardShower : MonoBehaviour
{
    public event Action OnCollectAllReward;

    [SerializeField] private RewardIcon rewardIconOrigin;
    [SerializeField] private Transform container;

    [SerializeField] private Button confirmButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private float margin;
    
    const int maxIconRow = 4;
    const int maxIconColumn = 2;
    const int maxIconCount = 8;

    private RewardIcon[] rewardIcons;

    public void CreateRewardList()
    {
        int iconCount = maxIconColumn * maxIconRow;
        rewardIcons = new RewardIcon[iconCount];

        int currentRow = 1, currentColumn = 1;
        RectTransform rect;
        Vector2 pivot = new Vector2(0,1);
        for (int i = 0; i < iconCount; i++)
        {
            RewardIcon icon = Instantiate(rewardIconOrigin, container);
            rewardIcons[i] = icon;
            rewardIcons[i].gameObject.SetActive(false);

            rect = rewardIcons[i].GetComponent<RectTransform>();
            rect.pivot = pivot;
            rect.anchoredPosition = new Vector2(rect.sizeDelta.x * (currentRow - 1) + margin, -rect.sizeDelta.y * (currentColumn - 1));

            if (currentRow % maxIconRow == 0)
            {
                currentRow = 0;
                currentColumn++;
            }
            else
            {
                currentRow++;
            }
        }
    }
    
    public void ShowRewardPanel(RewardData[] rewards)
    {
        gameObject.SetActive(true);
        CreateRewardList();
        StartCoroutine(SetRewardIcons(rewards));
    }
    
    int completeCount;
    public IEnumerator SetRewardIcons(RewardData[] rewards)
    {
        completeCount = 0;

        while (completeCount < rewards.Length)
        {
            nextButton.gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(false);
            
            for(int i = 0; i < maxIconCount; i++)
            {
                rewardIcons[i].SetIcon(null, rewards[completeCount++].Value);
            }
            
            if(completeCount >= rewards.Length) break;
            
            yield return WaitForPressNextButton();
        }

        nextButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
    }

    bool isNextButtonPressed;
    private IEnumerator WaitForPressNextButton()
    {
        isNextButtonPressed = false;
        
        while (isNextButtonPressed == false)
        {
            yield return null;
        }
    }
    
    public void OnPressNextButton()
    {
        isNextButtonPressed = true;
    }
    
    public void OnPressConfirmButton()
    {
        OnCollectAllReward?.Invoke();
        gameObject.SetActive(false);
    }
}
