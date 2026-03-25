using System.Collections;

public interface IUIAnimation
{
    public void ActiveUIAnimation();

    public IEnumerator DeactiveUIAnimationCoroutine();
}
