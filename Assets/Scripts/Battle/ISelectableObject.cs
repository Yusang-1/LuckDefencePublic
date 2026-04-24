public interface ISelectableObject
{
    public void Selected();

    public void SelectedEnd();
    
    public SelectableController SelectableController { get; }
}
