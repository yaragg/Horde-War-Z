using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int health = 4;
	public int healthMax = 4;
	public Material full;
	public Material depleted;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) this.gameObject.transform.GetChild(1).GetComponent<Gun>().Shoot();
	}

	public void DecreaseHealth(){
		if(health>1){
			health--;
			GameObject heart = this.gameObject.transform.GetChild(0).GetChild(health).gameObject;
			Renderer rend = heart.GetComponent<Renderer>();
			rend.enabled = true;
			rend.sharedMaterial = depleted;
		}
		else{
			GameObject.Find("Player").GetComponent<Player>().decreaseCharacterCount();
			//Destroy(gameObject);
			gameObject.SetActive(false);
		}
	}

	public void IncreaseHealth(){
		if(health < healthMax){
			GameObject heart = this.gameObject.transform.GetChild(0).GetChild(health).gameObject;
			Renderer rend = heart.GetComponent<Renderer>();
			rend.enabled = true;
			rend.sharedMaterial = full;
			health++;
		}
	}

	public void Shoot(){

	}
}
