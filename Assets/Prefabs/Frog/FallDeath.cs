using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int fallDistance;
    [SerializeField] GameObject ScoreBoard;
    private Score score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < fallDistance)
        {
            // Die.
            // but move first so you don't like... break it.
            this.transform.position = new Vector2(0,0);
            ScoreBoard.GetComponent<Score>().AddPoint();
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}
