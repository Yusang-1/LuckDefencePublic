using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingBarUI : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadingPercentText;

    public void Initialize()
    {
        loadingSlider.value = 0;
        loadingPercentText.text = "0";
    }

    public void SetLoadingBar(float percent)
    {
        loadingSlider.value = percent;
        loadingPercentText.text = percent.ToString();
    }    
}
