using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogControlArcade : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int jump;
    [SerializeField] private float distance2GroundFlag; // Here's a flag for tracking distance to ground. 

    [SerializeField] Vector2 inputWatcher;





    private bool grounded; // If on the ground, grounded is true.
    private bool doubleJump;  // If true, let player jump again. 
    
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = new Vector2( this.transform.position.x + speed * inputWatcher.x * Time.deltaTime, this.transform.position.y);
        
        // Need to check if the player is on the ground or not. 
        
    }


    //private void OnMove(InputValue moveInput)
    //{
    //    Vector2 movementVector = moveInput.Get<Vector2>();
    //    inputWatcher = movementVector;
    //    Debug.Log(movementVector);
    //}

    //private void OnJump()
    //{
    //    //Debug.Log("Jumping");
    //    // Need to do jumping here.
    //    // This outta be something simple but... Let's do something like

    //}

}
