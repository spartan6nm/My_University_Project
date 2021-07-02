using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleCannonFire : MonoBehaviour
{

    #region Field Declerations
    [Header("Cannon Fire")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    [SerializeField] private float fireCD;
    [SerializeField] private float buletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private GameObject DeathEffect;
    private CannonBullet bulletController;
    private WaitForSeconds fireCooldown;
    #endregion

    #region Start Up

    private void Start()
    {
        fireCooldown = new WaitForSeconds(fireCD);
        StartCoroutine(FireCooldown());
    }
    private void OnDisable()
    {
    }

    #endregion


    #region Cannon Fire

    private void EnableFire()
    {
        CannonFire();
    }

    public void DisableFire()
    {
        StartCoroutine(FireCooldown()); // start cooldown
    }

    IEnumerator FireCooldown()
    {
        yield return fireCooldown;
        EnableFire();
    }

    private void CannonFire()
    {
        DisableFire();


        audioManager.Play("TurtleCannonFire");
        animator.SetTrigger("Fire");

        bulletController =
                Instantiate(bullet, firePoint.position, Quaternion.identity).GetComponent<CannonBullet>();
        bulletController.DeathEffect = DeathEffect;

        bulletController.facing = (gameObject.transform.rotation.y > 0 ? true : false);

        bulletController.gameObject.layer = LayerMask.NameToLayer("Enemy");
        bulletController.bulletDirection = -transform.right;
        bulletController.deathDelayf = bulletLifeTime;
        bulletController.bulletSpeed = buletSpeed;

        
    }
    #endregion
}
