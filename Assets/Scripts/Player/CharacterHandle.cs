using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandle : MonoBehaviour
{
    // jump , melee attack , range attack , animations , effects , sounds , hit point system


    #region Field Declarations


    [Header("Movement")]
    public float speed = 2;

    float right = 0, left = 0, x = 0;

    Vector2 direction = new Vector2(0, 0);

    bool moving = false;

    Rigidbody2D rigidBody;

    [Header("Jump")]
    public Transform ground;

    public LayerMask groundLayer;

    public float groundCheckRadius;

    public float JumpPower;




    [Header("Animation")]
    public Animator animator;




    #endregion


    #region Saved Codes as Comment
    //Debug.Log(GroundCheck());

    //Debug.Log(rigidBody.velocity);

    // getting input from UI buttons

    //Debug.Log("here");
    #endregion


    #region Unity ENgine Functions
    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        Move();
        MoveAnimations();
    }

    #endregion


    #region Movement

    
    public void Left()
    {
        left = -1;
    }

    public void Right()
    {
        right = 1;
    }

    public void EndMoveR()
    {
        right = 0;
    }
    public void EndMoveL()
    {
        left = 0;
    }

    public void JumpInput()
    {
        Jump();
    }


    // move is done by getting a final number from the sum of left and right UI buttons being peressed
    // that number is given as the x or horizontal value of a vector 2 direction
    private void Move()
    {
        x = left + right;
        
        if (x != 0)
        {
            direction = new Vector2(x*speed, rigidBody.velocity.y);

            moving = true;
            Facing((int)x);
            rigidBody.velocity = direction;
        } 
        else if (x==0)
        {
            moving = false;
        }
        
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


    public bool GroundCheck()
    {

        return Physics2D.OverlapCircle(ground.position, groundCheckRadius, groundLayer);
    }


    private void Jump()
    {
        if (GroundCheck())
        {
            rigidBody.velocity = Vector2.up * JumpPower;
            animator.SetBool("jumping", true);
        }
        
    }

    #endregion

    #region Animation

    private void MoveAnimations()
    {
        animator.SetBool("moving", moving);
        animator.SetFloat("move", x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            animator.SetBool("jumping", false);
        }
    }

    #endregion
}
