using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Take Damage")]
    [SerializeField] private float health;
    [SerializeField] private float takenDamage;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



    public void TakeHit(int type)
    {

        if(type == 1)
        {
            health -= takenDamage;
        }
        else
        {
            health -= takenDamage * 2;
        }

        if(health <= 0)
        {
            Die();
        }
        // do damage

        animator.SetTrigger("TakeHit");
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

