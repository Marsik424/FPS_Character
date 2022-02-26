using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class HandGun : MonoBehaviour, Gun
{
    [SerializeField] private int _maxAmountOfAmmo = 12;
    [SerializeField] private float _maxRange = 100f;
    [SerializeField] private float _damage = 7f;
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private float _fireEffectDelay = 1f;
    [SerializeField] private int _fireRate = 1;
    [SerializeField] private Transform _gunEnd;
    public float Damage { get => _damage; }
    public int FireRate { get => _fireRate; }
    public int AmountOfAmmo { get => _currentAmountOfAmmo; set => _currentAmountOfAmmo = value; }
    public int MaxAmountOfAmmo { get => _maxAmountOfAmmo;}

    private Camera _camera;
    private AudioSource _gunFireSound;
    private LineRenderer _lineRenderer;
    private RaycastHit _hit;
    private float _nextFire = 0f;
    private int _currentAmountOfAmmo = 0;

    private void Start()
    {
        _camera = GetComponentInParent<Camera>();
        _gunFireSound = GetComponent<AudioSource>();
        _lineRenderer = GetComponent<LineRenderer>();
        _currentAmountOfAmmo = _maxAmountOfAmmo;
    }
    public void Shoot()
    {
        if (Time.time > _nextFire && AmountOfAmmo != 0)
        {
            _nextFire = Time.time + _fireRate;
            StartCoroutine(ShotEffect());
            Vector3 _rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            _lineRenderer.SetPosition(0, _gunEnd.position);
            
            if (Physics.Raycast(_rayOrigin, _camera.transform.forward, out _hit, _maxRange))
                _lineRenderer.SetPosition(1, _hit.point);
            else
                _lineRenderer.SetPosition(1, _rayOrigin + (_camera.transform.forward * _maxRange));
            AmountOfAmmo--;
        }
        else if (AmountOfAmmo == 0)
            StartCoroutine(Reload());
    }
    public IEnumerator Reload()
    {
        //Start reload animation
        yield return new WaitForSeconds(_reloadTime);
        transform.gameObject.SetActive(true);
        AmountOfAmmo = _maxAmountOfAmmo;
    }
    private IEnumerator ShotEffect()
    {
        //play Sound
        _gunFireSound.pitch = Random.Range(0.8f, 1.2f);
        _gunFireSound.Play();
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(_fireEffectDelay);
        _lineRenderer.enabled = false;
    }

}
