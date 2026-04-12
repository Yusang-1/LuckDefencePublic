using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SelectPlatformUI : MonoBehaviour, IUIAnimation
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI entityName;
    [SerializeField] private Button promotionButton;

    [SerializeField] private float UIMoveSpeed;

    [SerializeField] private UIAnimation uiAnimation;

    [SerializeField] private int openDirection;

    [SerializeField] private float uiOpenTime;
    private IEnumerator deactiveUICoroutine;

    private bool isOpen;

    public bool IsOpen => isOpen;
    
    public IEnumerator Initialize()
    {
        yield return StartCoroutine(uiAnimation.Initizlize());

        promotionButton.interactable = false;
    }

    public void OpenUI(Platform platform)
    {
        SetData(platform);        

        if (isOpen) return;

        gameObject.SetActive(true);
        if (deactiveUICoroutine != null)
        {
            StopCoroutine(deactiveUICoroutine);
        }

        ActiveUIAnimation();
    }

    public void OnCloseUI()
    {
        deactiveUICoroutine = DeactiveUIAnimationCoroutine();
        StartCoroutine(deactiveUICoroutine);
    }

    public void SetData(Platform platform)
    {
        entityName.text = platform.Entities[0].Data.Code.ToString();
        promotionButton.interactable = platform.CheckIsPromotionable();
    }

    public void ActiveUIAnimation()
    {
        isOpen = true;

        uiAnimation.PlayEnableAnimation(uiOpenTime);
    }

    public IEnumerator DeactiveUIAnimationCoroutine()
    {
        yield return null;

        isOpen = false;
        uiAnimation.PlayDisableAnimation(uiOpenTime);

        while (true)
        {
            if (uiAnimation.IsDisableAnimationFinished == true)
            {
                break;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
