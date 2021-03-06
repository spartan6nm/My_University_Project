using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandle : MonoBehaviour
{
    // jump , melee attack , range attack , animations , effects , sounds , hit point system


    #region Field Declarations


    [Header("Movement")]
    [SerializeField]private float speed = 2;

    private float right = 0, left = 0, x = 0;

    private Vector2 direction = new Vector2(0, 0);

    private bool moving = false;

    private Rigidbody2D rigidBody;


    [Header("Jump")]
    public Transform ground;

    public LayerMask groundLayer;

    public float groundCheckRadius;

    public float JumpPower;

    private float yVelocity = 0;


    [SerializeField] private AudioManager audioManager;


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

        EventBroker.PlayerHited += TakeHit;
    }

    private void OnDisable()
    {
        EventBroker.PlayerHited -= TakeHit;
    }

    private void FixedUpdate()
    {
        
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

        KeepXInBounds();

        if (x != 0)
        {
            direction = new Vector2(x, yVelocity);

            moving = true;
            Facing((int)x);
            rigidBody.velocity = direction *speed* Time.deltaTime;
        } 
        else if (x==0)
        {
            moving = false;
        }
        
    }

    private void KeepXInBounds()
    {
        if (x > 1 || x < -1)
        {
            x = 0;
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
            audioManager.Play("Jump");
            yVelocity = JumpPower * Vector2.up.y;
            Invoke("ResetYVelocity", 0.2f);
            
            if (x != 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity* 0.19f);

                //rigidBody.AddForce(Vector2.up * JumpPower * 350);
            }
            else
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity * 22);
            }
            
            animator.SetBool("Jumping" , true);
        }
        
    }

    private void ResetYVelocity()
    {
        yVelocity = -Vector2.up.y;
    }


    #endregion


    #region Animation

    private void MoveAnimations()
    {
        animator.SetBool("moving", moving);
        animator.SetFloat("move", x);
    }

    #endregion


    #region Take Hit and Colliders
    private void TakeHit()
    {
        animator.SetTrigger("hitted");

        // take hit logic and UI animation
        // control health UI by sending notification to game manager and calling player took hit there so it can update the UI
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            audioManager.Play("Land");
            animator.SetBool("Jumping", false);
        }
    }

    #endregion
}
