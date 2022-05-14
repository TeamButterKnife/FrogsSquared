using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandigueAI : MonoBehaviour
{
    [SerializeField] GameObject parentPlat;
    public bool isAwake { get; private set; }

    private float secondsAwake;

    public float speed;

    private Vector2[] parentSides;
    private int activeSide;

    // Start is called before the first frame update
    void Start()
    {
        isAwake = true;
        secondsAwake = 0;

        activeSide = 0;
        parentSides = new Vector2[4];
        parentSides[0] = new Vector2(parentPlat.transform.position.x - parentPlat.transform.localScale.x / 2, parentPlat.transform.position.y - parentPlat.transform.localScale.y / 2);
        parentSides[1] = new Vector2(parentPlat.transform.position.x + parentPlat.transform.localScale.x / 2, parentPlat.transform.position.y - parentPlat.transform.localScale.y / 2);
        parentSides[2] = new Vector2(parentPlat.transform.position.x + parentPlat.transform.localScale.x / 2, parentPlat.transform.position.y + parentPlat.transform.localScale.y / 2);
        parentSides[3] = new Vector2(parentPlat.transform.position.x - parentPlat.transform.localScale.x / 2, parentPlat.transform.position.y + parentPlat.transform.localScale.y / 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAwake)
            StartCoroutine(PatrolPlat());
    }

    IEnumerator PatrolPlat()
    {
        Debug.Log(secondsAwake);
        while (isAwake)
        {
            if (secondsAwake >= 60f)
            {
                isAwake = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, GetNextPosition(), speed);
            secondsAwake += .02f;
            yield return new WaitForSecondsRealtime(20f);
        }
    }

    private Vector2 GetNextPosition()
    {
        if (new Vector2(transform.position.x, transform.position.y) == parentSides[activeSide])
        {
            
            activeSide++;
            if (activeSide == 4)
            {
                activeSide = 0;
            }
        }
        return parentSides[activeSide];
    }
}
