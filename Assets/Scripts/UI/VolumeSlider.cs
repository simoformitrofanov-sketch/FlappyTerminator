using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private const float MinVolume = 0.0001f;
    private const float DecibelMultiplier = 20f;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _parameterName;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (_mixer.GetFloat(_parameterName, out float decibels))
        {
            float volume = Mathf.Pow(10f, decibels / DecibelMultiplier);

            _slider.SetValueWithoutNotify(
                volume <= MinVolume ? 0f : volume);
        }

        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float volume)
    {
        float decibels = Mathf.Log10(
            Mathf.Max(volume, MinVolume)) * DecibelMultiplier;

        _mixer.SetFloat(_parameterName, decibels);
    }
}