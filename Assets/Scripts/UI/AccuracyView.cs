using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class AccuracyView : MonoBehaviour
{
    [SerializeField] private AccuracyCounter _counter;
    [SerializeField] private string _label = "ACCURACY";

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _counter.Changed += OnAccuracyChanged;
        OnAccuracyChanged(_counter.Value);
    }

    private void OnDisable()
    {
        _counter.Changed -= OnAccuracyChanged;
    }

    private void OnAccuracyChanged(float value)
    {
        _text.text = $"{_label}:{value:0}%";
    }
}