using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Field Declarations

    public Vector3 projectileDirection;
    public float projectileSpeed;
    public bool isPlayers;

    private Rigidbody2D rigidbody;

    #endregion


    #region Movement

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {

        rigidbody.velocity = projectileDirection * projectileSpeed;

        if (ScreenBounds.OutOfBounds(transform.position))
        {
            //if (isPlayers)
            //{
            //  EventBroker.CallProjectileoutOfBounds();
            //}
            //Destroy(gameObject);
        }
    }
    #endregion
}
