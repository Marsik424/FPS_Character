using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Gun
{
    public float Damage { get; }
    public int FireRate { get; }
    public int AmountOfAmmo { get; }
    public int MaxAmountOfAmmo { get; }

    public void Shoot();
    public IEnumerator Reload();
}
