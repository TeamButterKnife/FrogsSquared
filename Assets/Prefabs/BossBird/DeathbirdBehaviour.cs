using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathbirdBehaviour : MonoBehaviour
{
    public BossState bossState { get; private set; }
    public SpriteRenderer spriteRenderer;
    public GameObject Player;
    public float differential;
    [SerializeField] int healthLeft = 3;

    float timeElapsed;
    public Sprite[] sprites = new Sprite[4];

    [SerializeField] float timeLimitTired;
    [SerializeField] float timeLimitRage;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossState = BossState.Normal;
    }

    void Update()
    {
        if(Player.transform.position.x - differential < transform.position.x)
        {
            // Set sprite to face left
            if(bossState == BossState.Rage){
                spriteRenderer.sprite = sprites[3];
            }else{
                
                spriteRenderer.sprite = sprites[1];
            }
            spriteRenderer.flipX = true;
        }else if(Player.transform.position.x + differential > transform.position.x){
            if(bossState == BossState.Rage){
                spriteRenderer.sprite = sprites[3];
            }else{
                
                spriteRenderer.sprite = sprites[1];
            }
            spriteRenderer.flipX = false;
            // set sprite to face right
        }else if(Player.transform.position.x < -differential && Player.transform.position.x > differential){
            spriteRenderer.sprite = sprites[0];
            // set sprite to face down
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (bossState)
        {
            case BossState.Tired:
                if (timeElapsed >= timeLimitTired)
                {
                    timeElapsed = 0f;
                    bossState = BossState.Normal;
                    Debug.Log("State change: " + bossState.ToString());
                    
                }
                break;
            case BossState.Normal:
                if (timeElapsed >= timeLimitTired)
                {
                    timeElapsed = 0f;
                    bossState = BossState.Tired;
                    Debug.Log("State change: " + bossState.ToString());
                    
                }
                break;
            case BossState.Rage:
                if (timeElapsed >= timeLimitRage)
                {
                    Debug.Log("Hello Rage");
                    spriteRenderer.sprite = sprites[3];
                    timeElapsed = 0f;
                    bossState = BossState.Normal;
                    FeatherSpawner[] spawners = GetComponentsInChildren<FeatherSpawner>(true);

                    foreach (FeatherSpawner featherSpawner in spawners) { 
                        featherSpawner.ResetSpawner();
                        if (featherSpawner.gameObject.activeInHierarchy)
                            featherSpawner.gameObject.SetActive(false);
                        else
                            featherSpawner.gameObject.SetActive(true);
                    }
                    Debug.Log("State change: " + bossState.ToString());
                    
                }
                break;
        }
        Debug.Log(bossState.ToString() + " state");
        Debug.Log(timeElapsed++);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.name.Contains("Frog") || collision.transform.name.Contains("Tongue"))
        {
            if (collision.transform.name.Contains("Feather"))
            {
                FeatherHitSelf();
            }
            return;
        }
            

        switch (bossState)
        {
            case BossState.Tired:
                //Deal damage
                timeElapsed = 0f;
                healthLeft--;
                if (healthLeft == 0)
                {
                    Debug.Log("ded");
                    gameObject.SetActive(false);
                    //Kill boss
                    return;
                }
                FollowPlayerNonTrigger[] feathers = GetComponentsInChildren<FollowPlayerNonTrigger>(true);
                foreach (FollowPlayerNonTrigger feather in feathers)
                    Destroy(feather);
                //Do not kill boss, go to Rage state
                bossState = BossState.Rage;
                BumpPlayer(collision);
                Debug.Log("State change: " + bossState.ToString());
                break;
            case BossState.Normal:
                //Bounce
                BumpPlayer(collision);
                break;
            case BossState.Rage:
                //Kill
                FindObjectOfType<FrogDeath>().Die();
                break;
        }
    }

    private void BumpPlayer(Collision2D collision)
    {
        if (collision.transform.position.x > transform.position.x)
        {
            //left
            collision.rigidbody.AddForce(Vector2.right * 50 , ForceMode2D.Impulse);
        } else
        {
            //right
            collision.rigidbody.AddForce(Vector2.left * 50, ForceMode2D.Impulse);
        }
    }

    public void FeatherHitSelf()
    {
        Debug.Log("Hit self");
    }

    public enum BossState
    {
        Tired,
        Normal,
        Rage
    }
}
