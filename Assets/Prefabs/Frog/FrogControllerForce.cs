using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FrogControllerForce : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] float jump = 1.0f;

    private float movementX;
    private float movementY;

    private int isLimitX, isLimitY;

    private bool isJumping;
    private bool isGrounded;

    // Private
    private Rigidbody2D frogRB; 

    // Start is called before the first frame update
    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>(); // get the frog's RB.
        isLimitX = 1;
        isLimitY = 1;
        isJumping = false;
    }

    public void Update()
    {

        // Grounded check

        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (groundCheck.collider is null)
        {
            Debug.Log("Not on floor");
        }

        if (frogRB.velocity.x > 5f || frogRB.velocity.x < -5f)
        {
            isLimitX = 0;
        } else
        {
            isLimitX = 1;
        }
        if (frogRB.velocity.y > 5f || frogRB.velocity.y < -5f)
        {
            isLimitY = 0;
        } else
        {
            isLimitY = 1;
        }

        Vector3 movement = new Vector3(movementX * isLimitX, movementY * isLimitY, 0f);
        frogRB.AddForce(movement * speed);
        //Debug.Log(frogRB.velocity);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector  = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        //Debug.Log(movementVector);
    }
    
    private void OnJump()
    {
        // if (!isJumping)
        // {
        //     movementY = 1f;
        //     isJumping = true;
        //     Vector3 movement = new Vector3(0f, movementY , 0f);
        //     frogRB.AddForce(movement * speed);
        // } else
        // {
        //     movementY = 0f;
        // }
        

        

    }

    
}
