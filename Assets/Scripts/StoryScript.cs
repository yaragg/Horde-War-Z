using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryScript : MonoBehaviour {

	public GameObject test;

	// Use this for initialization
	void Start () {
        Text testDisplay = test.GetComponent<Text>();
        testDisplay.text = "";
		for(int i=0; i<4; i++){
			testDisplay.text += NameScript.PlayerNames[i] + " ";

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
