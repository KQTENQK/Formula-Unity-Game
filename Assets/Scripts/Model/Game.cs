using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private FieldBuilder _fieldBuilder;
    [SerializeField] private FieldView _fieldView;
    [SerializeField] private MenuPresenter _menuPresenter;
    [SerializeField] private WinScreenPresenter _winScreenPresenter;

    private Queue<IReadOnlyCell> _selectedCells;
    private Field _field;
    private int _emptyCellsCount;

    private void Start()
    {
        _selectedCells = new Queue<IReadOnlyCell>();
    }

    private void OnEnable()
    {
        _menuPresenter.StartingGame += OnStartingGame;
        _winScreenPresenter.RestartingGame += OnRestartingGame;
    }

    private void OnDisable()
    {
        _menuPresenter.StartingGame -= OnStartingGame;
        _winScreenPresenter.RestartingGame -= OnRestartingGame;
    }

    private void OnStartingGame()
    {
        ResetGame();
    }

    private void OnRestartingGame()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        if (_field != null)
        {
            for (int x = 0; x < _field.GetLength(0); x++)
            {
                for (int y = 0; y < _field.GetLength(1); y++)
                {
                    _field[x, y].TryingSelect -= OnCellTryingSelect;
                    _field[x, y].Deselected -= OnCellDeselected;
                }
            }
        }

        _emptyCellsCount = 0;
        _selectedCells.Clear();
        _field = _fieldBuilder.Build();

        for (int x = 0; x < _field.GetLength(0); x++)
        {
            for (int y = 0; y < _field.GetLength(1); y++)
            {
                _field[x, y].TryingSelect += OnCellTryingSelect;
                _field[x, y].Deselected += OnCellDeselected;
            }
        }
    }

    private void OnCellTryingSelect(IReadOnlyCell cell)
    {
        const int maxSelectedCells = 2;

        if (_selectedCells.Count == 1 && _selectedCells.Peek() == cell)
        {
            _selectedCells.Dequeue().Deselect();
            return;
        }

        _selectedCells.Enqueue(cell);
        cell.Select();

        if (_selectedCells.Count == maxSelectedCells)
        {
            IReadOnlyCell first = _selectedCells.Dequeue();
            IReadOnlyCell second = _selectedCells.Dequeue();

            second.Deselect();
            first.Deselect();

            if (first.Block.EqualFormulaValue == second.Block.FormulaValue || second.Block.EqualFormulaValue == first.Block.FormulaValue)
            {
                first.SetEmpty();
                second.SetEmpty();

                _emptyCellsCount += 2;
                _selectedCells.Clear();
            }    
        }

        if (_emptyCellsCount >= _field.Count)
            EndGame();
    }

    private void EndGame()
    {
        _winScreenPresenter.ShowScreen();
    }

    private void OnCellDeselected(IReadOnlyCell cell)
    {
        _selectedCells.Clear();
    }
}
