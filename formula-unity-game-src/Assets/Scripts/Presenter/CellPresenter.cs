using UnityEngine;
using UnityEngine.UI;

public class CellPresenter : MonoBehaviour
{
    [SerializeField] private CellView _cellView;
    [SerializeField] private CellAnimation _cellAnimation;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private float _changingColorDuration;
    [SerializeField] private float _shrinkingDuration;

    private Color _normalColor;
    private IReadOnlyCell _cell;

    private void OnEnable()
    {
        _cellView.CellClick += OnCellClick;
    }
    private void OnDestroy()
    {
        _cellView.CellClick -= OnCellClick;
        _cell.BecameEmpty -= OnCellBecameEmpty;
        _cell.Selected -= OnCellSelected;
        _cell.Deselected -= OnCellDeselected;
    }

    private void OnCellClick()
    {
        _cell.TrySelect();
    }

    private void OnCellBecameEmpty()
    {
        _cellView.SetInteractable(false);
        _cellAnimation.DestroyAnimation(_shrinkingDuration, () => Destroy(gameObject));
    }

    private void OnCellSelected(IReadOnlyCell cell)
    {
        _cellView.ChangeColor(_normalColor, _selectedColor, _changingColorDuration);
    }

    private void OnCellDeselected(IReadOnlyCell cell)
    {
        _cellView.ChangeColor(_selectedColor, _normalColor, _changingColorDuration);
    }

    public void BindCell(IReadOnlyCell cell)
    {
        _cell = cell;
        _cellView.SetText(cell.Block.FormulaValue);
        _cell.BecameEmpty += OnCellBecameEmpty;
        _cell.Selected += OnCellSelected;
        _cell.Deselected += OnCellDeselected;
        _normalColor = _cellView.gameObject.GetComponent<Image>().color;
    }

    public void SetVisible(bool state)
    {
        _cellView.gameObject.SetActive(state);
    }
}
