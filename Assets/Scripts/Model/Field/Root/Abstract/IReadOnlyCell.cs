using UnityEngine.Events;

public interface IReadOnlyCell
{
    public int Row { get; }
    public int Column { get; }
    public Block Block { get; }
    public bool IsSelected { get; }
    public bool IsEmpty { get; }

    public event UnityAction<IReadOnlyCell> Selected;
    public event UnityAction<IReadOnlyCell> TryingSelect;
    public event UnityAction<IReadOnlyCell> Deselected;
    public event UnityAction BecameEmpty;

    public void Select();
    public void TrySelect();
    public void Deselect();
    public void SetEmpty();
}
