public interface IHoldableObject
{
    public bool Holded();

    public void HoldReleased(bool isHoldSuccess = true);
}
