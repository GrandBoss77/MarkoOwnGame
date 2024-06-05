using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    private Enemy enemyScript;

    [SerializeField] ParticleSystem footstepEffectL;
    [SerializeField] ParticleSystem footstepEffectR;
    [SerializeField] AudioClip footstepSound;

    private void Start()
    {
        enemyScript = GetComponentInParent<Enemy>();
    }

    private void OnLeftFootDown()
    {
        if (footstepEffectL == null) { return; }

        footstepEffectL.Play();
    }

    private void OnRightFootDown()
    {
        if (footstepEffectR == null) { return; }

        footstepEffectR.Play();
    }

    private void DealDamage()
    {
        enemyScript.DealDamage();
    }

    private void EndAttack()
    {
        enemyScript.EndAttack();
    }
}
