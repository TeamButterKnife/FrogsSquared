using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogControlArcade : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int jump;

    [SerializeField] Vector2 inputWatcher;
    
    void Start()
    {
        inputWatcher = GetComponent<Vector2>();
    }

    void Update()
    {
        this.transform.position = new Vector2( this.transform.position.x + speed * inputWatcher.x * Time.deltaTime, this.transform.position.y);
    }


    private void OnMove(InputValue moveInput)
    {
        Vector2 movementVector = moveInput.Get<Vector2>();
        inputWatcher = movementVector;
        Debug.Log(movementVector);
    }

}
