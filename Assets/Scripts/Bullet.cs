using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float moveSpeed = 10.0f;
	float timeCreated;
	public float lifespan = 4;
	public int damage = 1;
	public bool persist = false;

	// Use this for initialization
	void Start () {
		timeCreated = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(transform.position + transform.right);
		transform.Translate(transform.up * moveSpeed * Time.deltaTime);

		//Destroy if time limit exceeded
		if(Time.timeSinceLevelLoad - timeCreated > lifespan) Destroy(this.gameObject);
	}

	// Called whenever the bullet collides with a 2D collider component
	void OnTriggerEnter2D(Collider2D col){
		// If the obj is an Enemy
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<Zombie>().DecreaseHealth(damage);
			var gameScript = GameObject.Find("gameScriptHolder").GetComponent<GameScriptScore>();
			gameScript.updateScoreKill(col.gameObject.GetComponent<Zombie>().score);
			if (!persist) { Destroy(this.gameObject); }

		}
	}
}
