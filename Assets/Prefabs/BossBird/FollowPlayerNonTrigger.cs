using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerNonTrigger : MonoBehaviour
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
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed);
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Frog")
        {
            Player.GetComponent<FrogDeath>().Die();
        }
        if (collision.gameObject.name == "Boss")
        {
            FindObjectOfType<DeathbirdBehaviour>().FeatherHitSelf();
        }
        if (!collision.gameObject.name.Contains("Feather"))
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.Player = player;
    }
}
