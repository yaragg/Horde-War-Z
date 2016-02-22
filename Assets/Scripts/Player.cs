using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] characters;
	public float moveSpeed = 7.5f;

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
		characters [0].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y - characters [0].transform.localScale.y, 0 );
		characters [1].transform.localPosition = new Vector3 (this.transform.position.x + characters [0].transform.localScale.x, this.transform.position.y, 0 );
		characters [2].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y + characters [0].transform.localScale.y, 0 );
		characters [3].transform.localPosition = new Vector3 (this.transform.position.x  - characters [0].transform.localScale.x, this.transform.position.y, 0 );
	}
}
