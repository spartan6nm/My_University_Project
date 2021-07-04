using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour , IBulletBehave
{
    #region Field Declarations

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject destroyedEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [HideInInspector] public Vector3 projectileDirection;
    [HideInInspector] public float deathDelayf;
    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public bool isPlayers;
    [HideInInspector] public bool facing;
    private EnemyHealth enemyHealth;

    private WaitForSeconds deathDelay;

    #endregion


    #region Movement

    private void Start()
    {
        deathDelay = new WaitForSeconds(deathDelayf);
        spriteRenderer.flipX = facing;
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
        Instantiate(destroyedEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);


    }


    #endregion

    #region Trigger Detection


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = collision.GetComponent<EnemyHealth>();

            enemyHealth.TakeHit(2);
            Die();
        }
    }


    #endregion
}
