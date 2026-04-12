using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform myRect;

    [SerializeField] private Vector3 originPosition;
    [SerializeField] private Vector3 destinationPosition;    

    private Vector3 originPos;
    private Vector3 destinPos;
    private bool isDisableAnimationFinished;

    public bool IsDisableAnimationFinished => isDisableAnimationFinished;

    public IEnumerator Initizlize()
    {
        if (myRect != null)
        {
            myRect.anchoredPosition = destinationPosition;
            yield return new WaitForFixedUpdate();
            destinPos = transform.localPosition;

            myRect.anchoredPosition = originPosition;
            yield return new WaitForFixedUpdate();
            originPos = transform.localPosition;
        }
        else
        {
            destinPos = destinationPosition;
            originPos = originPosition;
        }        
    }

    public void PlayEnableAnimation(float time)
    {
        transform.DOLocalMove(destinPos, time).SetEase(Ease.OutSine);
    }

    public void PlayDisableAnimation(float time)
    {
        IsAnimationFinished(false);

        transform.DOLocalMove(originPos, time).SetEase(Ease.OutSine).OnComplete(() => IsAnimationFinished(true));
    }

    private void IsAnimationFinished(bool value)
    {
        isDisableAnimationFinished = value;
    }
}
