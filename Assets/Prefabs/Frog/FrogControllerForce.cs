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

    private int isLimitX;
    private int JUMP_MOD;

    [SerializeField] float jumpLimit;
    [SerializeField] float tongueLimit;

    // Private
    private Rigidbody2D frogRB;
    private SpringJoint2D frogSJ;

    // Start is called before the first frame update
    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>(); // get the frog's RB.
        frogSJ = GetComponent<SpringJoint2D>();
        isLimitX = 1;
        JUMP_MOD = 2;
    }

    public void Update()
    {
        // Grounded check

        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, LayerMask.GetMask("Ground"));
        if (groundCheck.collider is null)
        {
            JUMP_MOD = 0;
        } else
        {
            JUMP_MOD = 2;
        }

        if (frogRB.velocity.x > 5f || frogRB.velocity.x < -5f)
        {
            isLimitX = 0;
        } else
        {
            isLimitX = 1;
        }

        Vector3 movement = new Vector3(movementX * isLimitX, movementY * JUMP_MOD, 0f);
        frogRB.AddForce(movement * speed, ForceMode2D.Impulse);
        if (frogRB.velocity.y > jumpLimit) frogRB.velocity = new Vector2(frogRB.velocity.x, jumpLimit);
        
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector  = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    private void OnFire()
    {
        RaycastHit2D hitCheck = Physics2D.Raycast(transform.position, Vector2.left, tongueLimit, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.left, Color.white, 3);
        if (hitCheck.collider is not null)
        {
            Debug.Log("Test");
            Debug.Log(hitCheck.point);
            frogSJ.connectedBody = hitCheck.rigidbody;
            
            frogSJ.connectedAnchor = hitCheck.transform.InverseTransformPoint(hitCheck.point);
            frogSJ.enabled = true;
        } else
        {
            Debug.Log("Not tested");
            frogSJ.enabled = false;
        }
    }
}
