using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlteanuCamera : MonoBehaviour
{
    public Transform target;
    public Transform sprite1, sprite2, sprite3, sprite4, sprite5;
    public float sprite2Amount, sprite3Amount, sprite4Amount, sprite5Amount;
    public float minHeight, maxHeight, minWide, maxWide;
    private Vector2 lastPos;
    private void Start()
    {
        if (target is null)
        {
            target = FindObjectOfType<FrogControllerForce>().transform;
        }
        lastPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, minWide, maxWide), Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
        sprite1.position = sprite1.position + new Vector3(amountToMove.x,amountToMove.y,0f);
        sprite2.position += new Vector3(amountToMove.x , amountToMove.y, 0f) * sprite2Amount;
        sprite3.position += new Vector3(amountToMove.x , amountToMove.y, 0f) * sprite3Amount;
        sprite4.position += new Vector3(amountToMove.x , amountToMove.y, 0f) * sprite4Amount;
        sprite5.position += new Vector3(amountToMove.x , amountToMove.y, 0f) * sprite5Amount;
        lastPos = transform.position;
    }
}
