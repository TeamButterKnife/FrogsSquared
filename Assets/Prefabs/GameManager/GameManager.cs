using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GlobalTimekeeper gameClock;
    [SerializeField] private float gameTimeLimit;
    void Awake()
    {
        gameClock.SetStartTime();
        gameClock.GameTimeLimit = gameTimeLimit;
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        // SceneManager.LoadScene("Olteanu Level", LoadSceneMode.Additive);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
