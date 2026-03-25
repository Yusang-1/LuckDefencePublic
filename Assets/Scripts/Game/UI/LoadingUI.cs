using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private LoadingBarUI loadingBarUI;    
    [SerializeField] private TextMeshProUGUI pressScreenText;
    [SerializeField] private Button pressScreenButton;

    private Tween blinkText;
    private bool isPressScreen;

    public LoadingBarUI LoadingBarUI => loadingBarUI;
    public bool IsPressScreen => isPressScreen;

    private void Start()
    {        
        loadingBarUI.Initialize();

        pressScreenText.gameObject.SetActive(false);
        pressScreenButton.enabled = false;
        isPressScreen = false;

        if (blinkText == null)
        {
            blinkText = pressScreenText.DOFade(0, 2.2f).SetLoops(200).SetEase(Ease.InOutSine);
            blinkText.Pause();
        }
    }

    private void OnDisable()
    {
        if (blinkText != null)
        {
            blinkText.Kill();
        }
    }

    public void LoadingCompleted()
    {
        pressScreenText.gameObject.SetActive(true);
        isPressScreen = false;
        pressScreenButton.enabled = true;

        if (blinkText != null)
        {
            blinkText.Play();
        }
    }

    public void OnPressScreen()
    {
        isPressScreen = true;
    }
}
