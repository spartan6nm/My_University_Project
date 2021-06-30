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
    [SerializeField] private float swingCD;
    private bool canSwingSword = true;
    private WaitForSeconds swingCooldown;
    private EnemyHealth enemyHealth;

    [Header("RangeAttack")]
    [SerializeField] private GameObject projectilePreFab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fireCD;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    private ProjectileController projectileController;
    private WaitForSeconds fireCooldown;
    private bool canDoRange = true;

    

    #endregion

    #region Unity Functions

    private void Start()
    {
        canSwingSword = true;
        fireCooldown = new WaitForSeconds(fireCD);
        swingCooldown = new WaitForSeconds(swingCD);
    }
    private void LateUpdate()
    {
    
    }
    private void OnEnable()
    {
        StartCoroutine(FireCooldown());
        StartCoroutine(SwingCooldown());
    }

    private void OnDisable()
    {

        StopAllCoroutines();
        EnableMeleeAttack();
        EnableRangeAttack();
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

            MeleeAttack();
        }   
    }
    /*
    private void CanSwingSword()
    {
        canSwingSword = true;
    }

    private void SwingAnimationStart()
    {
        Invoke("CanSwingSword", 0.5f);
    }
    */
    #endregion

    #region Attack Methods


    private void MeleeAttack()
    {
        if(canSwingSword)
        {
            DisableMeleeAttack();
            animator.SetTrigger("melee");

            Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hittedEnemies)
            {
                enemyHealth = enemy.GetComponent<EnemyHealth>();

                enemyHealth.TakeHit(1);
            }

            StartCoroutine(SwingCooldown()); // swing cooldown
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
            projectileController.isPlayers = true;
            projectileController.projectileDirection = transform.right;
            projectileController.deathDelayf = projectileLifeTime;
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

    private void DisableMeleeAttack()
    {

        canSwingSword = false;
    }

    private void EnableMeleeAttack()
    {

        canSwingSword = true;
    }

    IEnumerator FireCooldown()
    {
        yield return fireCooldown;
        EnableRangeAttack();
    }
    IEnumerator SwingCooldown()
    {
        yield return swingCooldown;
        EnableMeleeAttack();
    }
    #endregion


}
