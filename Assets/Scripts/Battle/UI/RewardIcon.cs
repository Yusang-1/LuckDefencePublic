using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;

    public void SetIcon(RewardInfo info, int count)
    {
        icon.sprite = info.Sprite;
                
        char[] textValue = CachedTextCombiner.CombineTexts(
            CachedTextCombiner.PushBackChar(info.RewardName, ' ', out int length1), 0, length1,
            CachedTextNumber.GetCachedText(count, out int length2), 0, length2, 'x'
            );
        text.SetCharArray(textValue);
        gameObject.SetActive(true);
    }

    public void ResetIcon()
    {
        gameObject.SetActive(false);
    }
}
