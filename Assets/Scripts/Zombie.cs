using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public int health = 2;
	public int healthMax = 2;

	public int score = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DecreaseHealth(){
		if(health > 1){
			health--;
		}
		else{
			Destroy(this.gameObject);
		}
	}

	public void IncreaseHealth(){
		if(health < healthMax){
			health++;
		}
	}

	// Called whenever the Zombie collides with a 2D collider component
	void OnTriggerEnter2D(Collider2D col){
		// If the obj is a character
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Character>().DecreaseHealth();
			this.DecreaseHealth();
		}
	}
}
