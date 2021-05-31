using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    #region Field Declerations
    [Header("MeleeAttackComponents")]
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange;
    public Animator animator;

    #endregion


    #region UI Input

    public void AttackInput()
    {
        MeleeAttack();
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


    #endregion


}
