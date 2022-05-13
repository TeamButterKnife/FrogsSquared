using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    // TMPro.` textMesh;
    // Text text;
    public int score;
    public ScriptieBoi scriptieBoi;
    private TextMeshProUGUI TMPWord;
    

    void Start()
    {
        // textMesh = GetComponent<TextMesh>();
        // textMesh.text = "butttttts";
        // text = GetComponent<Text>();
        // text.text = "Hello world";
        // score = testSO.iHoldStuff;
        score = scriptieBoi.iHoldStuff;
        TMPWord = GetComponent<TextMeshProUGUI>();
        TMPWord.text = score.ToString();
    }

    // Update is called once per frame
    public void AddPoint()
    {
        score++;
        scriptieBoi.iHoldStuff++;
        Debug.Log(score);
        // Should probably also send information to a scriptable object.  Oh yeah, how do we do that?
    }
}
