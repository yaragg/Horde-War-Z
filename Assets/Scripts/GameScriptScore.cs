using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScriptScore : MonoBehaviour {

    public GameObject hudText;

    public static int currScore;

	// For Zombie spawning
    float timeLastSpawned = 0;
	float waveSpawned = 0;
    public float zombieTimer = 1f;
	public float waveTimer = 1.5f;
    public GameObject zombieType;
    GameObject[] zombieSpawners;
    public float spawnDistance = 10f;
	public int waveSize = 10;

	// Use this for initialization
	void Start () {
        currScore = 0;
        StartCoroutine(UpdateScore());
        zombieSpawners = GameObject.FindGameObjectsWithTag("Spawner");
    }
	
	// Update is called once per frame
	void Update () {
        Text scoreDisplay = hudText.GetComponent<Text>();
        scoreDisplay.text = "Score " + currScore.ToString("D5");

		// Spawns a single zombie at a time
		if((Time.timeSinceLevelLoad - timeLastSpawned) >= zombieTimer) {
			spawnZombies(zombieType, spawnLocation(), 1);
            timeLastSpawned = Time.timeSinceLevelLoad;
        }

		// Spawns waves of zombies
		if((Time.timeSinceLevelLoad - waveSpawned) >= waveTimer) {
			spawnZombies(zombieType, spawnLocation(), waveSize);
			waveSpawned = Time.timeSinceLevelLoad;
			waveSize += 2;
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

    public Vector3 spawnLocation(){
        Vector3 loc;
        int rand = Random.Range(0, zombieSpawners.Length);
        while (Vector3.Magnitude(zombieSpawners[rand].transform.position - GameObject.Find("Player").transform.position) < spawnDistance)
        {
            rand = Random.Range(0, zombieSpawners.Length);
        }
        loc = zombieSpawners[rand].transform.position;
		loc.z = 1;
        return loc;
    }

	void spawnZombies(GameObject zombieType, Vector3 location,  int numZombies){
		for (int i = 0; i < numZombies; i ++){
			GameObject zombie = (GameObject)Instantiate(zombieType, location, Quaternion.identity);
			zombie.GetComponent<AIMovement>().moveSpeed = Random.Range(1.0f, 4.0f);
			waveSpawned = Time.timeSinceLevelLoad;
		}
	}
}
