using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int fallDistance;
    // [SerializeField] GameObject ScoreBoard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < fallDistance)
        {
            GetComponent<FrogDeath>().Die();
        }
    }
}
