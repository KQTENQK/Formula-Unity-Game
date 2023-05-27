using UnityEngine;

public class Field
{
    private Vector2Int _fieldSize;
    private Cell[,] _cells;

    public Field(Vector2Int fieldSize, Cell[,] cells)
    {
        _fieldSize = fieldSize;
        _cells = cells;
    }

    public Vector2Int FieldSize => _fieldSize;
    public int Count => _cells.Length;

    public IReadOnlyCell this[int row, int column]
    {
        get
        {
            return _cells[row, column];
        }
    }

    public int GetLength(int dimension)
    {
        return _cells.GetLength(dimension);
    }
}
