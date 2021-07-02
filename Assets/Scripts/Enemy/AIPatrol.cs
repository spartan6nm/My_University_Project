using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{



    #region Field Declarations
    [Header("Movement")]
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Transform circleDetectionPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float circleRadius;
    [SerializeField] private float idleDuration;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    private bool directionRL; // true for right and false for left
    private Vector2 direction = Vector2.right;
    public bool patroling;

    #endregion

    #region Unity funcs
    void Awake()
    {

        patroling = true;
        directionRL = true;
    }


    void FixedUpdate()
    {
        GroundChecker();

        if(patroling)
        {
            Patroling();
        }
        
    }

    #endregion

    #region Patroling

    private void Patroling()
    {
        rigidbody.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    private void Facing(int direction)
    {
        switch (direction)
        {
            case -1:
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
        }
    }

    private void GroundChecker()
    {
        //detecting the end of the platform
        if (Physics2D.OverlapCircle(circleDetectionPoint.position, circleRadius, groundLayer) == false)
        {
            if (directionRL)
            {
                patroling = false;
                StartCoroutine(Iidle(-1));
            }
            else
            {
                patroling = false;
                StartCoroutine(Iidle(1));
            }
        }
    }


    IEnumerator Iidle(int facing)
    {
        yield return new WaitForSeconds(idleDuration);
        IdleEnd(facing);
    }


    private void IdleEnd(int facing)
    {
        if(facing == 1)
        {
            Facing(1);
            direction = Vector2.right;
            directionRL = true;
            patroling = true;
        }
        else
        {
            Facing(-1);
            direction = Vector2.left;
            directionRL = false;
            patroling = true;
        }
    }

    #endregion


    #region PlayerCollision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            EventBroker.CallPlayerHitted();
        }
        
    }

    #endregion
}
