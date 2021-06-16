using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Transform circleDetectionPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float circleRadius;
    [SerializeField] private LayerMask groundLayer;
    private bool directionRL; // true for right and false for left
   

        

    private Vector2 direction = Vector2.right;

    public bool patroling;


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

        if (Physics2D.OverlapCircle(circleDetectionPoint.position, circleRadius, groundLayer) == false)
        {

            Debug.Log("here");
            if (directionRL)
            {
                patroling = false;
                Facing(-1);
                direction = Vector2.left;
                directionRL = false;
                patroling = true;

            }
            else
            {
                patroling = false;
                Facing(1);
                direction = Vector2.right;
                directionRL = true;
                patroling = true;
            }

        }

    }



    #endregion
}
