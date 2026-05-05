using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI count;
    
    CachedTextNumber cachedText;
    public void SetIcon(Sprite icon, int count)
    {
        this.icon.sprite = icon;
        
        if(cachedText == null)
        {
            cachedText = new CachedTextNumber();            
        }
        this.count.SetCharArray(cachedText.GetCachedText(count, out int length), 0, length);
    }
}
