using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMushroom : MonoBehaviour
{
    private Animator anim;
    public float jumpingForceToAdd;
    [HideInInspector][SerializeField]public FrogControllerForce theFrog;
    [SerializeField] float jumpingLimitTime;
    [HideInInspector][SerializeField] float jumpingLimitCounter;


    private void Start()
    {
        anim = GetComponent<Animator>();
        theFrog = FindObjectOfType<FrogControllerForce>();
    }

    private void Update()
    {
        if(jumpingLimitCounter > 0)
        {
            jumpingLimitCounter -= Time.deltaTime;
            if(jumpingLimitCounter <= 0)
            {
                theFrog.jumpLimit = 20f;
                theFrog.jump = 20f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theFrog.jumpLimit = 50;
            theFrog.jump = 50;
            theFrog.frogRB.velocity = new Vector2(theFrog.frogRB.velocity.x, jumpingForceToAdd);
            jumpingLimitCounter = jumpingLimitTime;
            AudioManager.instance.PlaySFX(22);
            anim.SetTrigger("bounceUP");
        }
    }

}
