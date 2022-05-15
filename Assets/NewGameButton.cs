using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void onClick()
    {
        Debug.Log("Hello world");
        Debug.Log("Hello world");
        Debug.Log("Hello world");
    }

    void OnMouseDown()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex+1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
}
