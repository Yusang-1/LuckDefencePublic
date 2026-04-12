using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LowerUI : MonoBehaviour, ILobbyUIState, IUIAnimation
{
    [SerializeField] private UIAnimation uiAnimation;
    [SerializeField] private float uiOpenTime;
    private IEnumerator deactiveUICoroutine;
    
    public IEnumerator Initialize()
    {
        yield return uiAnimation.Initizlize();
    }

    public void ActiveUI()
    {
        gameObject.SetActive(true);
        //if(deactiveUICoroutine != null)
        //{
        //    StopCoroutine(deactiveUICoroutine);
        //}

        //ActiveUIAnimation();
    }

    public void DeactiveUI()
    {
        gameObject.SetActive(false);

        //deactiveUICoroutine = DeactiveUIAnimationCoroutine();
        //StartCoroutine(deactiveUICoroutine);
    }

    public void ActiveUIAnimation()
    {
        uiAnimation.PlayEnableAnimation(uiOpenTime);
    }

    public IEnumerator DeactiveUIAnimationCoroutine()
    {
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
