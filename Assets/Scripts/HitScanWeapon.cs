using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    public int weaponDamage = 20;
    public ParticleSystem HitParticle = null;
    public ParticleSystem MuzzleFlash = null;

    protected new void Start()
    {
        base.Start();
    }
    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            AudioManager.PlaySound(reloadSound, MuzzleFlash.transform.position, 0.5f);
            return false;
        }
        HitScanFire();

        return true;
    }

    private void HitScanFire()
    {
        AudioManager.PlaySound(shootSound, MuzzleFlash.transform.position, 0.3f);
        MuzzleFlash.Play();
   
        Ray weaponRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new();

        if (Physics.Raycast(weaponRay, out hit, WeaponRange, ~IgnoreHitMask))
        {
            HitParticle.Play();
            HitParticle.transform.SetParent(null);

            HitParticle.transform.position = hit.point;
            HitParticle.transform.forward = hit.normal;
            HitParticle.transform.Translate(hit.normal.normalized * 0.1f);

            HandleEntityHit(hit);
        }
    }

    private void HandleEntityHit(RaycastHit hit)
    {
        if (hit.collider)
        {
            if(hit.transform.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(weaponDamage, Vector3.zero, 0.3f );
            }
        }
    }
}
