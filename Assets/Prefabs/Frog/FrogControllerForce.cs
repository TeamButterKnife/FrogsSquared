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

    public SpriteRenderer spriteRenderer { get; private set; }
    // Private
    [HideInInspector][SerializeField]public Rigidbody2D frogRB;
    private SpringJoint2D frogSJ;
    private Camera camera;
    private LineRenderer lineRenderer;
    private GameObject tongueObject;

    /*
    void Awake()
    {
    #   if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 15;
    #   endif
    }
    */

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

    public void FixedUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs( GetComponent<Rigidbody2D>().velocity.x));
        Vector3 movement = GetMovement();
        frogRB.AddForce(movement * speed, ForceMode2D.Impulse);
        if (frogRB.velocity.y > jumpLimit) frogRB.velocity = new Vector2(frogRB.velocity.x, jumpLimit);
        
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, new Vector2(transform.position.x, transform.position.y + transform.localScale.y/8));
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
        Vector2 mousePointRelative = transform.InverseTransformPoint(camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + transform.localScale.y/8) + GetFacingDirection(mousePointRelative)*0.5f;

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
        AudioManager.instance.PlaySFX(17);
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

        //if (Mathf.Abs(mouseRelativePosition.x) > Mathf.Abs(mouseRelativePosition.y))
        //{ //Left or Right
        //    if (mouseRelativePosition.x > 0)
        //    {//Right
        //        return Vector2.right;
        //    }
        //    else
        //    {//Left
        //        return Vector2.left;
        //    }
        //}
        //else
        //{ //Top or Down
        //    if (mouseRelativePosition.y > 0)
        //    {//Top
        //        return Vector2.up;
        //    }
        //    else
        //    {//Down
        //        return Vector2.down;
        //    }
        //}


        //The same code but now with rounding of 45 degrees.
        Vector2 up_right = new Vector2(1, 1).normalized;
        Vector2 down_right = new Vector2(1, -1).normalized;
        Vector2 down_left = new Vector2(-1, -1).normalized;
        Vector2 up_left = new Vector2(-1, 1).normalized;

        //Directions of middle sections (this probably have a better solution than this crap) -> maybe a dictionary?
        //Think of those as directions from this graph: https://i.imgur.com/piiy9xZ.png
        //They are defined so I can choose the quadrants around the eith directions and round the values with that in mind
        //Vector2 up_right_up = new Vector2(1, 2).normalized;
        //Vector2 up_right_right = new Vector2(2, 1).normalized;
        //Vector2 down_right_right = new Vector2(2, -1).normalized;
        //Vector2 down_right_down = new Vector2(1, -2).normalized;
        //Vector2 down_left_down = new Vector2(-1, -2).normalized;
        //Vector2 down_left_left = new Vector2(-2, -1).normalized;
        //Vector2 up_left_left = new Vector2(-2, 1).normalized;
        //Vector2 up_left_up = new Vector2(-1, 2).normalized;

        //Ok those turned out to be completely useless. Still going to leave them here commented because they could be useful

        Vector2 mouseNorm = mouseRelativePosition.normalized;


        if (mouseNorm.x > 0 && mouseNorm.y > 0)
        { // First quadrant

            if (Vector2.Dot(mouseNorm, up_right) < 0.90f && Vector2.Dot(mouseNorm, down_right) > -0.3f) 
            {
                return Vector2.right;
            } 
            else if (Vector2.Dot(mouseNorm, up_right) < 0.90f && Vector2.Dot(mouseNorm, down_right) < -0.3f)
            {
                return Vector2.up;
            } 
            else
            {
                return up_right;
            }

        } else if (mouseNorm.x > 0 && mouseNorm.y < 0)
        { // Forth quadrant

            if (Vector2.Dot(mouseNorm, down_right) < 0.90f && Vector2.Dot(mouseNorm, down_left) > -0.3f)
            {
                return Vector2.down;
            }
            else if (Vector2.Dot(mouseNorm, down_right) < 0.90f && Vector2.Dot(mouseNorm, down_left) < -0.3f)
            {
                return Vector2.right;
            }
            else
            {
                return down_right;
            }

        } else if (mouseNorm.x < 0 && mouseNorm.y < 0)
        { //Third quadrant

            if (Vector2.Dot(mouseNorm, down_left) < 0.90f && Vector2.Dot(mouseNorm, up_left) > -0.3f)
            {
                return Vector2.left;
            }
            else if (Vector2.Dot(mouseNorm, down_left) < 0.90f && Vector2.Dot(mouseNorm, up_left) < -0.3f)
            {
                return Vector2.down;
            }
            else
            {
                return down_left;
            }

        } else if (mouseNorm.x < 0 && mouseNorm.y > 0)
        { //Second quadrant

            if (Vector2.Dot(mouseNorm, up_left) < 0.90f && Vector2.Dot(mouseNorm, up_right) > -0.3f)
            {
                return Vector2.up;
            }
            else if (Vector2.Dot(mouseNorm, up_left) < 0.90f && Vector2.Dot(mouseNorm, up_right) < -0.3f)
            {
                return Vector2.left;
            }
            else
            {
                return up_left;
            }

        } else
        { //SPECIFIC POSITIONS -> Almost impossible to happen, perfect clicks. Just return the value normalized
            //This happens if the x or y are perfectly zero. This is really hard as those are float values.
            return mouseNorm;
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
        //Debug.Log("I'm jumpsing");
        animator.SetBool("isJumping", true);
        AudioManager.instance.PlaySFX(23);
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

    public void Steep1()
    {
        AudioManager.instance.PlaySFX(01);
    }
    public void Steep2()
    {
        AudioManager.instance.PlaySFX(02);
    }
    public void Steep3()
    {
        AudioManager.instance.PlaySFX(03);
    }
    public void Steep4()
    {
        AudioManager.instance.PlaySFX(04);
    }
}
