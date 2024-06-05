using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponState WeaponType = WeaponState.Total;
    public int Ammunition = 1337;
    public int CurrentAmmunition;
    public AudioClip shootSound = null;
    public AudioClip reloadSound = null;
    public float reloadTime = 1f;

    public float WeaponRange = 13337.0f;
    public LayerMask IgnoreHitMask = 0;

    private bool isReloading = false; // Flag to check if reloading is in progress

    protected void Start()
    {
        CurrentAmmunition = Ammunition;
    }

    public virtual bool Fire()
    {
        if (CurrentAmmunition < 1 && !isReloading) // Check if not already reloading
        {
            StartCoroutine(Reload());
            return false;
        }
        CurrentAmmunition--;
        return true;
    }

    public IEnumerator Reload()
    {
        AudioManager.PlaySound(reloadSound, transform.position, 0.5f);
        Debug.Log("Reloading");
        isReloading = true; // Set the flag to true to indicate reloading has started
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmunition = Ammunition;
        isReloading = false; // Reset the flag to false to indicate reloading has finished
    }
}


