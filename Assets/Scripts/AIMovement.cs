using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    public float moveSpeed = 6.0f;
	public float maxDist = 8.0f;
    GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    Vector3 toPlayer = new Vector3();
		//if (!(toPlayer.magnitude > maxDist)) {
			toPlayer = player.transform.position - this.transform.position;
			this.transform.Translate (toPlayer.normalized * moveSpeed * Time.deltaTime);
		//}
	}
}
