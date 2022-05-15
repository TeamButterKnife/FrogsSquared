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
    }

    private void OnEnable()
    {
        mandigAI = GetComponentInParent<MandigueAI>(true);
        transform.localPosition = (mandigAI.transform.InverseTransformPoint(mandigAI.getParentPlat().transform.position));
    }
}
