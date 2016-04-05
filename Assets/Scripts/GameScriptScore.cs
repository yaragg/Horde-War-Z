﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScriptScore : MonoBehaviour {

    public GameObject hudText;

    public static int currScore;

    float timeLastSpawned = 0;
    public float zombieTimer = 1f;
	public int spawnNum = 1;

	float timeAlive;

    public GameObject zombieType;
    GameObject[] zombieSpawners;
    public float spawnDistance = 10f;
	public float maxZombies = 50.0f;

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

		if((Time.timeSinceLevelLoad - timeLastSpawned) >= zombieTimer && GameObject.FindGameObjectsWithTag("Enemy").Length < maxZombies) {
			spawnZombie(spawnLocation(), spawnNum);
			timeLastSpawned = Time.timeSinceLevelLoad;
        }

		if (timeAlive % 20 == 0) {spawnNum ++;}
		if (spawnNum > 4) {spawnNum = 4;}
		
	}

	public void updateScoreKill(int score){
		currScore += score;		
	}

    IEnumerator UpdateScore() {
        for(;;){
            timeAlive += 1;
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
		//while (Vector3.Magnitude(zombieSpawners[rand].transform.position - GameObject.Find("Player").transform.position) < spawnDistance) 
        //{
            //rand = Random.Range(0, zombieSpawners.Length);
        //}
        loc = zombieSpawners[rand].transform.position;
        return loc;
    }

	void spawnZombie(Vector3 location, int numZombies){
		for (int i = 0; i < numZombies; i++){
			GameObject zombie = (GameObject)Instantiate(zombieType, location, Quaternion.identity);
		}
	}
}
