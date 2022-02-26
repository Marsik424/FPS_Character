using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    public bool _isReloading = false;
    private Gun _gun;
    void Start()
    {
        _gun = GetComponentInChildren<Gun>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShootFromWeapon();
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }
    private void ShootFromWeapon() => _gun.Shoot();
    private void Reload()
    {
        if (!_isReloading && _gun.AmountOfAmmo != _gun.MaxAmountOfAmmo)
        {
            _isReloading = true;
           
            StartCoroutine(_gun.Reload());
        }
    }

}
