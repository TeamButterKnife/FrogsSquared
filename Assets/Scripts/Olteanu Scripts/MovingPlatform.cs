using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public int currentPoint;

    public Transform platform;

    private void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, speed * Time.deltaTime);

        if(Vector3.Distance(platform.position, points[currentPoint].position) < .06f)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
