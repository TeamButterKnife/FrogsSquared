using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Animations;


public class FrogControllerForce : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [HideInInspector][SerializeField] public float jump = 1.0f;

    private float movementX;
    private float movementY;

    private int cont;

    private int isLimitX;
    private int JUMP_MOD;
    [SerializeField] int WALL_JUMP_STRENGTH = 1;
    [SerializeField] int GROUND_JUMP_STRENGTH = 2;
    private Vector2 JUMP_KICK;
    private float lastJumpTime;
    [SerializeField] float JUMP_KICK_STRENGTH = 0.9f;

    [HideInInspector][SerializeField] public float jumpLimit;
    [SerializeField] float tongueLimit;
    [SerializeField] GameObject TongueBulletPrefab;
    [SerializeField] Animator animator;

    // Private
    [HideInInspector][SerializeField]public Rigidbody2D frogRB;
    private SpringJoint2D frogSJ;
    private Camera camera;
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    private GameObject tongueObject;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frogRB = GetComponent<Rigidbody2D>(); // get the frog's RB.
        frogSJ = GetComponent<SpringJoint2D>();
        camera = FindObjectOfType<Camera>();
        lineRenderer = GetComponent<LineRenderer>();
        isLimitX = 1;
        JUMP_MOD = 2;
    }

    private Vector3 GetMovement()
    {
    #region groundchecks
        //Grounded check

        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.55f * transform.localScale.y, LayerMask.GetMask("Ground"));
        RaycastHit2D leftWallCheck = Physics2D.Raycast(transform.position, Vector2.left, 0.55f, LayerMask.GetMask("Ground"));
        RaycastHit2D rightWallCheck = Physics2D.Raycast(transform.position, Vector2.right, 0.55f, LayerMask.GetMask("Ground"));

        //Debug.Log(cont);

        if (groundCheck.collider is null)
        {
            // We are not grounded. Reset jump modifiers and check for walls.
            JUMP_MOD = 0; // If we don't find a wall, we won't be able to jump. Simple.
            JUMP_KICK = Vector2.zero;
            animator.SetBool("isJumping", true);
            if (!(leftWallCheck.collider is null) && movementY > 0) // Fail silently if not pressing jump
            {
                JUMP_MOD = WALL_JUMP_STRENGTH;
                JUMP_KICK = Vector2.right;
            }
            else if (!(rightWallCheck.collider is null) && movementY > 0)
            {
                JUMP_MOD = WALL_JUMP_STRENGTH;
                JUMP_KICK = Vector3.left;
            }
        } else
        {
            JUMP_MOD = GROUND_JUMP_STRENGTH;
            JUMP_KICK = Vector2.zero;
            animator.SetBool("isJumping", false);
        }

        if (frogRB.velocity.x > 5f || frogRB.velocity.x < -5f)
        {
            isLimitX = 0;
        } else
        {
            isLimitX = 1;
        }
        #endregion
        Vector2 movement2d = new Vector2(movementX * isLimitX, movementY * JUMP_MOD);
        movement2d += JUMP_KICK * JUMP_KICK_STRENGTH;
        Vector3 movement = new Vector3(movement2d.x, movement2d.y, 0f);
        return movement;
    }

    public void Update()
    {
        animator.SetFloat("speed", Mathf.Abs( GetComponent<Rigidbody2D>().velocity.x));
        Vector3 movement = GetMovement();
        frogRB.AddForce(movement * speed, ForceMode2D.Impulse);
        if (frogRB.velocity.y > jumpLimit) frogRB.velocity = new Vector2(frogRB.velocity.x, jumpLimit);
        
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, new Vector2(transform.position.x, transform.position.y + transform.localScale.y/2));
            lineRenderer.SetPosition(1, tongueObject.transform.position);
        }

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector  = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        if(movementX > 0){
            spriteRenderer.flipX = false;
        }else if(movementX == 0){
            // This just keeps us from changing the flipX value by accident.
        }else{
            spriteRenderer.flipX = true;
            // sp
        }
    }

    
    
    private void OnFire()
    {
        if (tongueObject != null)
        {
            Destroy(tongueObject);
            lineRenderer.enabled = false;
            frogSJ.enabled = false;
            return;
        }
        //if (frogSJ.enabled == true) {
        //    frogSJ.enabled = false;
        //    return;
        //}
        
        //Subtracts mouse position from main character to get accurate values
        Vector2 mousePointRelative = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;

        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + transform.localScale.y/2) + GetFacingDirection(mousePointRelative)*0.5f;

        tongueObject = Instantiate(TongueBulletPrefab, spawnPosition, transform.rotation, transform);
        tongueObject.GetComponent<Rigidbody2D>().AddForce(GetFacingDirection(mousePointRelative)*10, ForceMode2D.Impulse);
        
        lineRenderer.enabled = true;
        
        //RaycastHit2D hitCheck = Physics2D.Raycast(transform.position, GetFacingDirection(mousePointRelative), tongueLimit, LayerMask.GetMask("Ground"));
        //if (!(hitCheck.collider is null))
        //{
        //    Debug.Log(hitCheck.point);
        //    frogSJ.connectedBody = hitCheck.rigidbody;

        //    frogSJ.connectedAnchor = hitCheck.transform.InverseTransformPoint(hitCheck.point);
        //    frogSJ.enabled = true;
        //}
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

    public void ActivateTongueSpring(ContactPoint2D contactPoint)
    {
        frogSJ.connectedBody = contactPoint.rigidbody;

        frogSJ.connectedAnchor = contactPoint.collider.transform.InverseTransformPoint(contactPoint.point);
        frogSJ.enabled = true;
    }

    private void OnJump()
    {
        Debug.Log("I'm jumpsing");
        animator.SetBool("isJumping", true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
