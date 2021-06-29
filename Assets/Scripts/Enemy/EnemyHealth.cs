using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Take Damage")]
    [SerializeField] private float health;
    [SerializeField] private float takenDamage;

    private TurtleCannonFire turtleScript;
    private bool isTrurtle = false;

    private Animator animator;


    private void Awake()
    {
        if(gameObject.GetComponent<TurtleCannonFire>() != null)
        {
            turtleScript = gameObject.GetComponent<TurtleCannonFire>();
            isTrurtle = true;
        }

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

        if (isTrurtle)
        {
            turtleScript.StopAllCoroutines();
            turtleScript.DisableFire();
        }
        animator.SetTrigger("TakeHit");
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

