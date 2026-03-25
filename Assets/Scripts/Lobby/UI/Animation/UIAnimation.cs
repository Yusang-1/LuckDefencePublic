using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    //[SerializeField] private RectTransform myRect;

    [SerializeField] private Vector3 originPosition;
    [SerializeField] private Vector3 destinationPosition;

    private bool isDisableAnimationFinished;

    public bool IsDisableAnimationFinished => isDisableAnimationFinished;

    public void PlayEnableAnimation(float time)
    {
        transform.DOLocalMove(destinationPosition, time).SetEase(Ease.OutSine);
    }

    public void PlayDisableAnimation(float time)
    {
        IsAnimationFinished(false);

        transform.DOLocalMove(originPosition, time).SetEase(Ease.OutSine).OnComplete(() => IsAnimationFinished(true));
    }

    private void IsAnimationFinished(bool value)
    {
        isDisableAnimationFinished = value;        
    }
}
