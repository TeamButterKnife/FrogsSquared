using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D senseBox;

    // [SerializeField] Vector2 inputWatcher;
    [SerializeField] private float speed;
    [SerializeField] private GameObject Player;
    void Start()
    {
        senseBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // bool isRight;
        // bool isUp;
        // int xMod;
        // int yMod; 

        // if
        
        // Debug.Log("Update");
        if(Player.transform.position.x < transform.position.x)
        {
            // isRight = false;
            // xMod = -1;
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            
        }else{
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            // isRight = true;
            // xMod = 1;
        }

        if(Player.transform.position.y < transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            // yMod = -1;
        }else{
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            // yMod = 1;
            // transform.position = new Vector2(transform.position.y + speed * Time.deltaTime, transform.position.y);
        }

        // transform.position = new Vector2(transform.position.y + (speed * yMod) * Time.deltaTime, transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Frog")
        {
            Player.GetComponent<FrogDeath>().Die();
        }
    }
}
