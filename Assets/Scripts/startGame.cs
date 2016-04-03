using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class startGame : MonoBehaviour {

    public GameObject playerNameField;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onStartClick()
    {
        InputField textField = playerNameField.GetComponent<InputField>();
        GlobalsScript.CurrPlayer = textField.text;
        NameScript.GeneratePlayerNames();
        Application.LoadLevel("StoryScene");
    }

    public void onStoryEndClick(){
        Application.LoadLevel("mainGameScene");
    }
}
