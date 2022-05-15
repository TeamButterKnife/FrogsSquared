using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampCamera : MonoBehaviour
{
    [SerializeField] GameObject FocusFab; // target prefab to clamp to.
    [SerializeField] float zoom = -10.0f; // used to make sure we don't put the camera on the wrong layer

    // Start is called before the first frame update
    void Start()
    {
        if (FocusFab is null)
        {
            FocusFab = FindObjectOfType<FrogControllerForce>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(FocusFab.transform.position.x, FocusFab.transform.position.y, zoom);
    }
}
