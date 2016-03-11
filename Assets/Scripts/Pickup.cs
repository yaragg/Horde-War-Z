using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public enum itemType{health, weapon};
	public itemType dropType;

	float timeCreated;
	public float lifespan = 4;

	// Use this for initialization
	void Start () {
		timeCreated = Time.timeSinceLevelLoad;
		dropType = itemType.health;
	}
	
	// Update is called once per frame
	void Update () {
		// Destroy if too old
		if(Time.timeSinceLevelLoad - timeCreated > lifespan) Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			switch (dropType){
				case itemType.health:
					if (col.gameObject.GetComponent<Character>().health != col.gameObject.GetComponent<Character>().healthMax){
						col.gameObject.GetComponent<Character>().IncreaseHealth();
						Destroy(this.gameObject);
					}
					break;
				case itemType.weapon:
					break;
			}
		}
	}
}
