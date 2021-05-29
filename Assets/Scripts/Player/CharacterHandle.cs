using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandle : MonoBehaviour
{
    // jump , melee attack , range attack , animations , effects , sounds , hit point system


    #region Field Declarations

    [Header("Movement")]
    Rigidbody2D rigidBody;

    public LayerMask groundLayer;

    public float speed = 2;

    float right = 0, left = 0, x = 0;

    Vector2 direction = new Vector2(0, 0);

    bool grounded , moving = false;



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

    private void Move()
    {
        x = left + right;
        if (left < 0 || right > 0)
        {
            moving = true;
        } 
        else if (left == 0 && right == 0)
        {
            moving = false;
        }

        MoveAnimations();

        direction = new Vector2(x, 0);
        rigidBody.velocity = direction * speed;
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
