using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldView : MonoBehaviour
{
    [SerializeField] private FieldBuilder _fieldBuilder;
    [SerializeField] private GridLayoutGroup _gridLayout;
    [SerializeField] private FieldAnimation _fieldAnimation;

    private void Start()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
    }

    private void OnEnable()
    {
        _fieldBuilder.BuiltField += OnBuiltField;
    }

    private void OnDisable()
    {
        _fieldBuilder.BuiltField -= OnBuiltField;
    }

    private void OnBuiltField()
    {
        _gridLayout.enabled = true;
        
        StartCoroutine(WaitFrameAndExecute(() => 
        {
            _gridLayout.enabled = false;
            _fieldAnimation.BeginAnimation();
        }));    
    }

    public IEnumerator WaitFrameAndExecute(UnityAction action)
    {
        yield return null;
        action?.Invoke();
    }
}
