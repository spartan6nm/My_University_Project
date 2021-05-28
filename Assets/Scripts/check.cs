using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    Rigidbody2D rigidBody;

    public LayerMask groundLayer;

    public float speed = 2;

    float right = 0, left = 0 , up = 0;

    bool grounded;


    Vector2 direction = new Vector2(0, 0);

    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

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
        if(grounded)
        {
            up = 10;
        }
    }


    private void FixedUpdate()
    {
        //Debug.Log(left + " || " + right);
        direction = new Vector2(left + right, 0);
        rigidBody.velocity = direction * speed;
    }
}
