using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float moveSpeed = 7.0f;
	float timeCreated;
	public float lifespan = 5;

	// Use this for initialization
	void Start () {
		timeCreated = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(transform.forward*moveSpeed*Time.deltaTime);

		//Destroy if time limit exceeded
		if(Time.timeSinceLevelLoad - timeCreated > lifespan) Destroy(this.gameObject);

		//TODO destroy if it collides with a zombie
	}
}
