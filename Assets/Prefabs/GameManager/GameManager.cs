using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GlobalTimekeeper gameClock;
    void Awake()
    {
        gameClock.SetStartTime();
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
