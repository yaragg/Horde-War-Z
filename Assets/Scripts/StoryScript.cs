using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryScript : MonoBehaviour {

	public GameObject storyContent;
	public string[] stories;

	void Awake(){
		TextAsset storyFile = Resources.Load("stories") as TextAsset;
        stories = storyFile.text.Split('#');
        Resources.UnloadAsset(storyFile);
	}

	// Use this for initialization
	void Start () {
        Text storyDisplay = storyContent.GetComponent<Text>();

        //Pick one of the stories
		int index = Random.Range(0, stories.Length);
		storyDisplay.text = stories[index];

		//Add in generated player names
		storyDisplay.text = storyDisplay.text.Replace("[A]", NameScript.Character_Names[3]);
		storyDisplay.text = storyDisplay.text.Replace("[B]", NameScript.Character_Names[1]);
		storyDisplay.text = storyDisplay.text.Replace("[C]", NameScript.Character_Names[2]);
		storyDisplay.text = storyDisplay.text.Replace("[D]", NameScript.Character_Names[0]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
