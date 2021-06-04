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
    [SerializeField] private ProjectileController projectileController;
    [SerializeField] private GameObject projectilePreFab;
    [SerializeField] private Transform shootingPoint;
    private bool canDoRange = true; 
    


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
        //canDoRange = false;

        animator.SetTrigger("range");

        ProjectileController projectile =
            Instantiate(projectilePreFab, shootingPoint.position , Quaternion.identity).GetComponent<ProjectileController>();

        projectile.gameObject.layer = LayerMask.NameToLayer("Player");
        projectile.isPlayers = true;
        projectile.projectileSpeed = 10;
        projectile.projectileDirection = transform.right;

    }

    public void EnableRangeAttack()
    {
        canDoRange = true;
    }
    #endregion


}
