using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Frog"))
        {
            transform.parent.GetComponent<FeatherSpawner>().ResetSpawner();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Frog"))
        {
            transform.parent.GetComponent<FeatherSpawner>().DeactivateSpawner();
        }
    }
}
