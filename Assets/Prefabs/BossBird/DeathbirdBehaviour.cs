using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathbirdBehaviour : MonoBehaviour
{
    public BossState bossState { get; private set; }
    [SerializeField] int healthLeft = 3;

    float timeElapsed;

    [SerializeField] float timeLimitTired;
    [SerializeField] float timeLimitRage;

    // Start is called before the first frame update
    void Start()
    {
        bossState = BossState.Normal;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (bossState)
        {
            case BossState.Tired:
                if (timeElapsed >= timeLimitTired)
                {
                    bossState = BossState.Normal;
                    Debug.Log("State change: " + bossState.ToString());
                    timeElapsed = 0f;
                    break;
                }
                break;
            case BossState.Normal:
                if (timeElapsed > 100000000f)
                {
                    bossState = BossState.Tired;
                    Debug.Log("State change: " + bossState.ToString());
                }
                break;
            case BossState.Rage:
                if (timeElapsed >= timeLimitRage)
                {
                    bossState = BossState.Normal;
                    FeatherSpawner[] spawners = GetComponentsInChildren<FeatherSpawner>();
                    foreach (FeatherSpawner featherSpawner in spawners)
                        featherSpawner.ResetSpawner();
                    Debug.Log("State change: " + bossState.ToString());
                    timeElapsed = 0f;
                    break;
                }
                break;
        }
        timeElapsed++;
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
                healthLeft--;
                if (healthLeft == 0)
                {
                    Debug.Log("ded");
                    gameObject.SetActive(false);
                    //Kill boss
                    return;
                }
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
