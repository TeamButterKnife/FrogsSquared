using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class FrogControllerForce : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] float jump = 1.0f;

    private float movementX;
    private float movementY;

    private int cont;

    private int isLimitX;
    private int JUMP_MOD;

    [SerializeField] float jumpLimit;
    [SerializeField] float tongueLimit;

    // Private
    private Rigidbody2D frogRB;
    private SpringJoint2D frogSJ;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>(); // get the frog's RB.
        frogSJ = GetComponent<SpringJoint2D>();
        camera = FindObjectOfType<Camera>();
        isLimitX = 1;
        JUMP_MOD = 2;
    }

    public void Update()
    {
        // Grounded check

        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, LayerMask.GetMask("Ground"));
        RaycastHit2D leftWallCheck = Physics2D.Raycast(transform.position, Vector2.left, 0.55f, LayerMask.GetMask("Ground"));
        RaycastHit2D rightWallCheck = Physics2D.Raycast(transform.position, Vector2.right, 0.55f, LayerMask.GetMask("Ground"));
        RaycastHit2D ceilingCheck = Physics2D.Raycast(transform.position, Vector2.up, 0.55f, LayerMask.GetMask("Ground"));

        //Debug.Log(cont);

        if (groundCheck.collider is null && leftWallCheck.collider is null && rightWallCheck.collider is null && ceilingCheck.collider is null)
        {
            JUMP_MOD = 0;
            StartCoroutine(JumpStopper());
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
        if (frogSJ.enabled == true) {
            frogSJ.enabled = false;
            return;
        }
        
        //Subtracts mouse position from main character to get accurate values
        Vector2 mousePointRelative = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;

        RaycastHit2D hitCheck = Physics2D.Raycast(transform.position, GetFacingDirection(mousePointRelative), tongueLimit, LayerMask.GetMask("Ground"));
        if (hitCheck.collider is not null)
        {
            Debug.Log(hitCheck.point);
            frogSJ.connectedBody = hitCheck.rigidbody;
            
            frogSJ.connectedAnchor = hitCheck.transform.InverseTransformPoint(hitCheck.point);
            frogSJ.enabled = true;
        }
    }

    public Vector2 GetFacingDirection(Vector2 mouseRelativePosition)
    {
       //Positive X and Y = Top and Right
       //X > Y = Right
       //Y > X = Top

       //Positive X and Negative Y = Right and Down
       //X > Y = Right
       //Y > X = Down

       //Negative X and Y = Down and Left
       //X > Y = Left
       //Y > X = Down

       //Negative X and Positive Y = Left and Top
       //X > Y = Left
       //Y > X = Top



       if (Mathf.Abs(mouseRelativePosition.x) > Mathf.Asin(mouseRelativePosition.y))
       { //Left or Right
           if (mouseRelativePosition.x > 0)
           {//Right
               return Vector2.right;
           }
           else
           {//Left
               return Vector2.left;
           }
       }
       else
       { //Top or Down
           if (mouseRelativePosition.y > 0)
           {//Top
               return Vector2.up;
           }
           else
           {//Down
               return Vector2.down;
           }
       }
    }


    IEnumerator JumpStopper()
    {
        yield return new WaitForSeconds(.1f);
    }
}
