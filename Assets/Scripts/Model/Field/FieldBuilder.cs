using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldBuilder : MonoBehaviour
{
    [Header("Field size: X for rows, Y for columns.")]
    [Space]
    [SerializeField] private Vector2Int _fieldSize;
    [SerializeField] private CellPresenter _cellPrefab;

    public event UnityAction BuiltField;

    public Field Build()
    {
        FieldGenerator fieldGenerator = new FieldGenerator();
        Field field = fieldGenerator.Generate(_fieldSize);

        for (int x = 0; x < field.GetLength(0); x++)
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                GameObject cell = Instantiate(_cellPrefab.gameObject, transform);
                CellPresenter cellPresenter = cell.GetComponent<CellPresenter>();

                cellPresenter.SetVisible(false);
                cellPresenter.BindCell(field[x, y]);
            }
        }

        BuiltField?.Invoke();

        return field;
    }
}
