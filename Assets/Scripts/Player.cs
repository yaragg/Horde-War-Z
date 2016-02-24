using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] characters;
	public float moveSpeed = 7.5f;
	int characterCount = 4;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Move the player
		var h = Input.GetAxis ("Horizontal");
		var v = Input.GetAxis ("Vertical");
		this.transform.Translate (new Vector3 (h, v, 0) * moveSpeed * Time.deltaTime);

		// Move all characters to updated position relative to the player
		if(characters[0].activeSelf) characters [0].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y - characters [0].transform.localScale.y, 0 );
		if(characters[1].activeSelf) characters [1].transform.localPosition = new Vector3 (this.transform.position.x + characters [0].transform.localScale.x, this.transform.position.y, 0 );
		if(characters[2].activeSelf) characters [2].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y + characters [0].transform.localScale.y, 0 );
		if(characters[3].activeSelf) characters [3].transform.localPosition = new Vector3 (this.transform.position.x  - characters [0].transform.localScale.x, this.transform.position.y, 0 );
	}

	public void decreaseCharacterCount(){
		characterCount--;

		if(characterCount <= 0)
			GameObject.Find("gameScriptHolder").GetComponent<GameScript>().onGameEnd();
	}
}
