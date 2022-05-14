using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandigueAwakenTrigger : MonoBehaviour
{
    MandigueAI mandigAI;
    private void Start()
    { 
        Vector3 vector = mandigAI.getParentPlat().transform.localScale + mandigAI.transform.localScale;
        GetComponent<BoxCollider2D>().size = new Vector2(vector.x, vector.y);
        //Set the size based on plat's size + mandigue's size
    }

    private void OnEnable()
    {
        Debug.Log("Enabled"); 
        mandigAI = GetComponentInParent<MandigueAI>(true);
        transform.localPosition = (mandigAI.transform.InverseTransformPoint(mandigAI.getParentPlat().transform.position));
        //Change BoxCollider from this object to fit the Floor/Wall piece automatically
        //
        //Change offset relative to the Mandigue
    }
}
