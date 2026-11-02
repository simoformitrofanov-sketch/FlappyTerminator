using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField, Range(0f, 1f)] private float _shootVolume = 0.5f;

    private AudioSource _effectsSource;

    public event Action<Bullet> Shot;

    public void Initialize(AudioSource effectsSource)
    {
        _effectsSource = effectsSource;
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(
            _bulletPrefab,
            _shootPoint.position,
            _shootPoint.rotation);

        Shot?.Invoke(bullet);
        bullet.Launch(_shootPoint.right, _targetLayers);
        _effectsSource.PlayOneShot(_shootSound, _shootVolume);
    }
}