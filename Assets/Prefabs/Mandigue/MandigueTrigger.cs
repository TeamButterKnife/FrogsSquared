using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandigueTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.name.Contains("Frog")) return;

        if (GetComponent<MandigueAI>().isAwake)
        {
            FindObjectOfType<FrogDeath>().Die();
        } else
        {
            GetComponent<MandigueAI>().setAwakenStatus(true);
        }
    }
}
