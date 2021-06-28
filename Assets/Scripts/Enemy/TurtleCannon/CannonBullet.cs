using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour , IBulletBehave
{
    #region Field Declarations

    [HideInInspector] public GameObject DeathEffect;
    [HideInInspector] public Vector3 bulletDirection;
    [HideInInspector] public float deathDelayf;
    [HideInInspector] public float bulletSpeed;
    private WaitForSeconds deathDelay;

    #endregion

    #region Start Up

    private void Start()
    {

        deathDelay = new WaitForSeconds(deathDelayf);

        StartCoroutine(DieCD());
    }


    #endregion

    #region Functions and Enums

    void Update()
    {
        Move();
    }


    public void Move()
    {
        transform.Translate(bulletDirection * Time.deltaTime * bulletSpeed);

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

        Instantiate(DeathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    #endregion

    #region Trigger and colliders

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Die();
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            EventBroker.CallPlayerHitted();
            Die();
        }
        
    }


    #endregion

}
