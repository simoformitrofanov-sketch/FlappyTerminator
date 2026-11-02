using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class StreakView : MonoBehaviour
{
    [SerializeField] private StreakCounter _counter;
    [SerializeField] private string _label = "STREAK";

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _counter.Changed += OnStreakChanged;
        OnStreakChanged(_counter.Value);
    }

    private void OnDisable()
    {
        _counter.Changed -= OnStreakChanged;
    }

    private void OnStreakChanged(int value)
    {
        _text.text = $"{_label}:{value}";
    }
}