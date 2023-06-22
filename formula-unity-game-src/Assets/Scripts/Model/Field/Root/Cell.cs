using UnityEngine;
using UnityEngine.Events;

public class Cell : IReadOnlyCell
{
    public int Row { get; private set; }
    public int Column { get; private set; }
    public Block Block { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsEmpty { get; private set; }

    public event UnityAction<IReadOnlyCell> Selected;
    public event UnityAction<IReadOnlyCell> TryingSelect;
    public event UnityAction<IReadOnlyCell> Deselected;
    public event UnityAction BecameEmpty;

    public Cell() 
    {
        IsEmpty = true;
    }

    public Cell(int row, int column) : this()
    {
        Row = row;
        Column = column;
    }

    public Cell(int row, int column, Block block) : this(row, column)
    {
        LinkBlock(block);
    }

    public void LinkBlock(Block block)
    {
        if (IsEmpty == false)
            throw new UnityException("Cell was not empty.");

        if (block is null)
            throw new System.ArgumentNullException("Linking block was null.");

        Block = block;
        IsEmpty = false;
    }

    public void TrySelect()
    {
        TryingSelect?.Invoke(this);
    }

    public void Select()
    {
        Selected?.Invoke(this);
    }

    public void Deselect()
    {
        Deselected?.Invoke(this);
    }

    public void SetEmpty()
    {
        IsEmpty = true;
        BecameEmpty?.Invoke();
    }
}
