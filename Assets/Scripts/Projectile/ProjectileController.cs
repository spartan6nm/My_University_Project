using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour , IBulletBehave
{
    #region Field Declarations

    [HideInInspector] public Vector3 projectileDirection;
    [HideInInspector] public LayerMask groundLayer;
    [HideInInspector] public float deathDelayf;
    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public bool isPlayers;
    
    private WaitForSeconds deathDelay;

    #endregion


    #region Movement

    private void Start()
    {
        deathDelay = new WaitForSeconds(deathDelayf);
        StartCoroutine(DieCD());
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(projectileDirection * Time.deltaTime * projectileSpeed);

    }
    #endregion

    #region Death
    public IEnumerator DieCD()
    {
        yield return deathDelay;
        Die();
    }

    public void Die()
    {
        // destroy self, play explosion animation

        Destroy(gameObject);


    }
    #endregion

    #region Trigger Detection

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // play explosion animation
            Die();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }


    #endregion
}
