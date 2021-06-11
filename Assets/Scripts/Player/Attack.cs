using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    #region Field Declerations
    [Header("MeleeAttackComponents")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange;
    [SerializeField] private Animator animator;
    private bool canSwingSword = true;

    [Header("RangeAttack")]
    [SerializeField] private GameObject projectilePreFab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fireCD;
    [SerializeField] private float projectileSpeed;
    private ProjectileController projectileController;
    private WaitForSeconds fireCooldown;
    private bool canDoRange = true;



    #endregion

    #region StartUp

    private void Start()
    {
        fireCooldown = new WaitForSeconds(fireCD);
    }

    #endregion

    #region UI Input

    public void RangeAttackInput()
    {
        if(canDoRange)
        {
            RangeAttack();
        }
        
    }


    public void AttackInput()
    {
        if(canSwingSword)
        {
            canSwingSword = false;
            MeleeAttack();
        }
        
    }

    private void CanSwingSword()
    {
        canSwingSword = true;
    }

    private void SwingAnimationStart()
    {
        Invoke("CanSwingSword", 0.14f);
    }
    #endregion

    #region Attack Methods


    private void MeleeAttack()
    {
        animator.SetTrigger("melee");
        
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hittedEnemies)
        {
            Debug.Log(enemy.name + " has been hit by sword");
        }
    }

    private void RangeAttack()
    {
        if(canDoRange)
        {
            DisableRangeAttack();

            animator.SetTrigger("range");

            projectileController =
                Instantiate(projectilePreFab, shootingPoint.position, Quaternion.identity).GetComponent<ProjectileController>();

            projectileController.gameObject.layer = LayerMask.NameToLayer("Player");
            projectileController.groundLayer = groundLayer;
            projectileController.isPlayers = true;
            projectileController.projectileDirection = transform.right;
            projectileController.deathDelayf = 2f;
            projectileController.projectileSpeed = projectileSpeed;

            StartCoroutine(FireCooldown()); // start cooldown
        }
        

    }

    private void DisableRangeAttack()
    {
        //play cooldonw animation? disable UI?
        canDoRange = false;
    }

    private void EnableRangeAttack()
    {
        // enable UI?
        canDoRange = true;
    }

    IEnumerator FireCooldown()
    {
        yield return fireCooldown;
        EnableRangeAttack();
    }
    #endregion


}
