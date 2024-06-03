using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    public int weaponDamage = 20;
    public ParticleSystem HitParticle = null;
    public ParticleSystem MuzzleFlash = null;
    //public AudioClip shootSound = null;

    protected new void Start()
    {
        base.Start();
    }
    public override bool Fire()
    {

        if (base.Fire() == false)
        {
            return false;
        }
        HitScanFire();

        return true;
    }

    private void HitScanFire()
    {
        //SoundManager.PlaySound(shootSound, transform.position);
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
       
            //HandleEntityHit(hit);
        }
    }

    //private void HandleEntityHit(RaycastHit hit)
    //{
    //    if (hit.collider)
    //    {
    //        Transform parentTransform = hit.collider.transform.parent;
    //        if (parentTransform != null)
    //        {
    //            if (parentTransform.TryGetComponent<Enemy>(out var enemy))
    //            {
    //                enemy.TakeDamage(weaponDamage);
    //            }
    //        }
    //    }
    //}
}
