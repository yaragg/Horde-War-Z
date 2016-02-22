using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public int health = 2;
	public int healthMax = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DecreaseHealth(){
		if(health>0){
			health--;
		}
		else{
			//TODO dead!
		}
	}

	public void IncreaseHealth(){
		if(health < healthMax){
			health++;
		}
	}
}
