using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandigueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.name.Contains("Frog")) return;

        if (GetComponent<MandigueAI>().isAwake)
        {
            Debug.Log("Kill");
            FindObjectOfType<FrogDeath>().Die();
        } else
        {
            Debug.Log("Not Kill");
            GetComponent<MandigueAI>().setAwakenStatus(true);
        }
    }
}
