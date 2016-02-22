using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreScript : MonoBehaviour {

    public GameObject scoreText;

    void Awake () {
    }

	// Use this for initialization
	void Start () {
        if (InitData()){
            Text scoreDisplay = scoreText.GetComponent<Text>();
            scoreDisplay.text = "High Score\n " + GlobalsScript.HighScoreName + "........" + GlobalsScript.HighScoreInt.ToString("D5");
        }
        else{
            Text scoreDisplay = scoreText.GetComponent<Text>();
            scoreDisplay.text = "High Score\n" + "HWZ........00000";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool InitData() {
        if (PlayerPrefs.HasKey("Name"))
        {
            GlobalsScript.HighScoreName = PlayerPrefs.GetString("Name");
            GlobalsScript.HighScoreInt = PlayerPrefs.GetInt("Score");
            return true;
        }

        else
            return false;
    }
}
