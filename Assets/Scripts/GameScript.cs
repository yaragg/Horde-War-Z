using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScript : MonoBehaviour {

    public GameObject hudText;

    public static int currScore;

    float timeLastSpawned = 0;

    public GameObject zombieType;

	// Use this for initialization
	void Start () {
        currScore = 0;

        StartCoroutine(UpdateScore());
	}
	
	// Update is called once per frame
	void Update () {
        Text scoreDisplay = hudText.GetComponent<Text>();
        scoreDisplay.text = "Score " + currScore.ToString("D5");

        if((Time.timeSinceLevelLoad - timeLastSpawned) >= 2) {
            Instantiate(zombieType, new Vector3(10, 10, 0), Quaternion.identity);
            timeLastSpawned = Time.timeSinceLevelLoad;
        }
	}

    IEnumerator UpdateScore() {
        for(;;){
            currScore += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    public void onGameEnd() {
       if (currScore > GlobalsScript.HighScoreInt) {
           PlayerPrefs.SetInt("Score", currScore);
           PlayerPrefs.SetString("Name", GlobalsScript.CurrPlayer);
       }
       Application.LoadLevel(0);
    }
}
