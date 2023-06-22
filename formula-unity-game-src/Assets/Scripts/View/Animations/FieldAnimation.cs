using System.Collections;
using UnityEngine;

public class FieldAnimation : MonoBehaviour
{
    [SerializeField] private float _secondsToMoveDownEachCell;
    [SerializeField] private float _delayBetweenEachCell;
    [SerializeField] private Canvas _canvas;

    public void BeginAnimation()
    {
        StartCoroutine(AnimateCells());
    }

    private IEnumerator AnimateCells()
    {
        yield return null;

        CellPresenter[] cellChildren = transform.GetComponentsInChildren<CellPresenter>();

        foreach (CellPresenter child in cellChildren)
        {
            child.SetVisible(true);

            CellAnimation cell = child.GetComponentInChildren<CellAnimation>();
            cell.transform.localPosition = new Vector3(cell.transform.localPosition.x,
                cell.transform.localPosition.y + _canvas.GetComponent<RectTransform>().rect.height,
                cell.transform.localPosition.z);
        }

        foreach (CellPresenter child in cellChildren)
        {
            yield return new WaitForSeconds(_delayBetweenEachCell);

            CellAnimation cell = child.GetComponentInChildren<CellAnimation>();

            Vector3 startPosition = cell.gameObject.transform.localPosition;
            Vector3 endPosition = new Vector3(cell.gameObject.transform.localPosition.x,
                cell.gameObject.transform.localPosition.y - _canvas.GetComponent<RectTransform>().rect.height,
                cell.gameObject.transform.localPosition.z);

            cell.StartAnimation(startPosition, endPosition, _secondsToMoveDownEachCell);
        }
    }
}
