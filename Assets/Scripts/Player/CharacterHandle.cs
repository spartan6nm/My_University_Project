using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandle : MonoBehaviour
{
    // jump , melee attack , range attack , animations , effects , sounds , hit point system


    #region Field Declarations


    [Header("Movement")]
    public LayerMask groundLayer;

    public float speed = 2;

    float right = 0, left = 0, x = 0;

    Vector2 direction = new Vector2(0, 0);

    bool grounded , moving = false;

    Rigidbody2D rigidBody;



    [Header("Animation")]
    public Animator animator; 




    #endregion

 
    #region Unity ENgine Functions
    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        
    }
    #endregion


    #region Movement

    // getting input from UI buttons
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

    public void Jump()
    {

    }


    // move is done by getting a final number from the sum of left and right UI buttons being peressed
    // that number is given as the x or horizontal value of a vector 2 direction
    private void Move()
    {
        x = left + right;
        if (x < 0 || x > 0)
        {
            moving = true;
            Facing((int)x);
        } 
        else if (x==0)
        {
            moving = false;
        }
        
        MoveAnimations();

        direction = new Vector2(x, 0);
        rigidBody.velocity = direction * speed;
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

    #endregion

    #region Animation

    private void MoveAnimations()
    {
        animator.SetBool("moving", moving);
        animator.SetFloat("move", x);
    }

    #endregion
}
