using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{



    #region Field Declarations
    [Header("Movement")]
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Transform circleDetectionPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float circleRadius;
    [SerializeField] private float idleDuration;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    private bool directionRL; // true for right and false for left
    private Vector2 direction = Vector2.right;
    public bool patroling;

    [Header("Anger Management")]
    [SerializeField] private SpriteRenderer spRenderer;
    [SerializeField] private bool detected;

    #endregion

    #region Unity funcs
    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();

        patroling = true;
        directionRL = true;
        detected = false;
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
        // detecting player for getting angry
        if (Physics2D.OverlapCircle(circleDetectionPoint.position, circleRadius + 1, playerLayer) == true && !detected)
        {
            detected = true;
            GetAngry();
            StartCoroutine(CalmDown());
        }else // starting to cooldown
        {
            detected = false;
        }

    }


    IEnumerator Iidle(int facing)
    {
        yield return new WaitForSeconds(idleDuration);
        IdleEnd(facing);
    }

    IEnumerator CalmDown()
    {
        yield return new WaitForSeconds(2);
        if(!detected)
        {
            Calm();
        }
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


    #region Anger Management

    private void GetAngry()
    {
        spRenderer.color = Color.red;
    }

    private void Calm()
    {
        spRenderer.color = Color.white;
    }


    #endregion


    #region PlayerCollision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventBroker.CallPlayerHitted();
    }

    #endregion
}
