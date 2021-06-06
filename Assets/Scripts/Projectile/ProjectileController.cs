using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Field Declarations

    [HideInInspector] public Vector3 projectileDirection;
    [HideInInspector] public LayerMask groundLayer;
    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public float deathDelayf;
    [HideInInspector] public bool isPlayers;

    private Rigidbody2D rigidbody;
    private WaitForSeconds deathDelay;

    #endregion


    #region Movement

    private void Start()
    {
        deathDelay = new WaitForSeconds(deathDelayf);
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(Death());
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {

        rigidbody.velocity = projectileDirection * projectileSpeed;

    }
    #endregion

    #region Death
    IEnumerator Death()
    {
        yield return deathDelay;
        Die();
    }

    private void Die()
    {
        // destroy self, play explosion animation

        Destroy(gameObject);


    }
    #endregion

    #region Trigger Detection

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            
            Die();
        }
    }


    #endregion
}
