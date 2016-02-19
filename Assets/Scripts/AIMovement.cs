using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    public float moveSpeed = 6.0f;
    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    Vector3 toPlayer = new Vector3();

        toPlayer = player.transform.position - this.transform.position;
        this.transform.Translate(toPlayer.normalized * moveSpeed * Time.deltaTime);
	}
}
