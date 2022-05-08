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



    // Private
    private Rigidbody2D frogRB; 

    // Start is called before the first frame update
    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>(); // get the frog's RB.
    }


    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector  = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        Debug.Log(movementVector);
        

    }
    // Update is called once per frame

    public void FixedUpdate()
    {
        // Debug.Log("Fixed Update");
        // Since I don't
        // Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Vector3 movement = new Vector3( movementX, 0.0f, movementY);
        frogRB.AddForce(movement * speed);
    }
}
