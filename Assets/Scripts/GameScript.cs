using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScript : MonoBehaviour {

    public GameObject hudText;

    public static int currScore;

	// Use this for initialization
	void Start () {
        currScore = 0;

        StartCoroutine(UpdateScore());
	}
	
	// Update is called once per frame
	void Update () {
        Text scoreDisplay = hudText.GetComponent<Text>();
        scoreDisplay.text = "Score " + currScore.ToString("D5");
	}

    IEnumerator UpdateScore() {
        for(;;){
            currScore += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    //public void onGameEnd() {
    //    if (currScore > GlobalsScript.HighScoreInt) {
    //        PlayerPrefs.SetInt("Score", currScore);
    //        PlayerPrefs.SetString("Name", GlobalsScript.CurrPlayer);
    //    }
    //    Application.LoadLevel(0);
    //}
}
