using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScriptScore : MonoBehaviour {

    public GameObject hudText;

    public static int currScore;

    float timeLastSpawned = 0;
    public float zombieTimer = 1f;

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

		if((Time.timeSinceLevelLoad - timeLastSpawned) >= zombieTimer) {
			Instantiate(zombieType, new Vector3(RandValue(10, 15), RandValue(10, 15), 0), Quaternion.identity);
            timeLastSpawned = Time.timeSinceLevelLoad;
        }
	}

	public void updateScoreKill(int score){
		currScore += score;		
	}

    IEnumerator UpdateScore() {
        for(;;){
            currScore += 1;
            yield return new WaitForSeconds(2f);
        }
    }

    public void onGameEnd() {
       if (currScore > GlobalsScript.HighScoreInt) {
           PlayerPrefs.SetInt("Score", currScore);
           PlayerPrefs.SetString("Name", GlobalsScript.CurrPlayer);
       }
       Application.LoadLevel(0);
    }
    
    public int RandValue(int min, int max) {
    	if (Random.Range(0, 2) == 1){
    		return(Random.Range(min, max) * -1);
    	}
    	else {
			return(Random.Range(min, max));
    	}
    }
}
