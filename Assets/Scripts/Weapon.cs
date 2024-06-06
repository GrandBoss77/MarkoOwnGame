using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponState WeaponType = WeaponState.Total;
    public int Ammunition = 1337;
    public int CurrentAmmunition;
    public float reloadTime = 1f;

    public float WeaponRange = 13337.0f;
    public LayerMask IgnoreHitMask = 0;

    private bool isReloading = false; // Flag to check if reloading is in progress
    public Audio audioInfo;

    protected void Start()
    {
        CurrentAmmunition = Ammunition;
    }

    public virtual bool Fire()
    {
        if (isReloading) // Check if currently reloading
        {
            Debug.Log("Cannot fire while reloading");
            return false;
        }

        if (CurrentAmmunition < 1) // Check if there's no ammunition left
        {
            StartCoroutine(Reload());
            return false;
        }

        CurrentAmmunition--;
        AudioManager.PlaySound(audioInfo.shootSound, transform.position, audioInfo.shootVolume);
        // Implement shooting logic here
        Debug.Log("Fired! Current Ammunition: " + CurrentAmmunition);
        return true;
    }

    public IEnumerator Reload()
    {
        AudioManager.PlaySound(audioInfo.reloadSound, transform.position, audioInfo.reloadVolume);
        Debug.Log("Reloading");
        isReloading = true; // Set the flag to true to indicate reloading has started
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmunition = Ammunition;
        isReloading = false; // Reset the flag to false to indicate reloading has finished
        Debug.Log("Reloaded! Current Ammunition: " + CurrentAmmunition);
    }

    [System.Serializable]
    public struct Audio
    {
        public AudioClip shootSound;
        public float shootVolume;
        public AudioClip reloadSound;
        public float reloadVolume;
    }
}