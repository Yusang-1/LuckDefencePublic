using UnityEngine;

public class SelectedCharactersContainer : MonoBehaviour
{
    [SerializeField] private RankUnlockSO rankUnlockData;
    [SerializeField] private CharRank rank;
    private bool isInitizlie;
    private void OnEnable()
    {
        if(isInitizlie == false) return;
        
        gameObject.SetActive(rankUnlockData.IsRankUnlocked(rank));
    }
    
    public void Initialize(CharRank rank)
    {
        this.rank = rank;
        isInitizlie = true;
    }
}
