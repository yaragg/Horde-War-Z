﻿using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public int health = 2;
	public int healthMax = 2;
    public GameObject nameBoxType;

    public int score = 5;

	public GameObject itemDrop;
	public float dropChance = 25.0f;

	public char genderChar = 'M';

	// Use this for initialization
	void Start () {

		genderChar = ChooseGender();

        GameObject nameBox = (GameObject)Instantiate(nameBoxType, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        nameBox.GetComponent<TextMesh>().text = NameScript.GetName(this.tag, genderChar);
        nameBox.transform.parent = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GameObject thisName = this.gameObject.transform.GetChild(0).gameObject;
        NameScript.ReturnName(this.tag, genderChar, thisName.GetComponent<TextMesh>().text);
    }

	public void DecreaseHealth(int damage){
		if(health > damage){
			health-=damage;
		}
		else{
			if (Random.Range (0.0f, 100.0f) <= dropChance) {
				GameObject.Instantiate(itemDrop, transform.position, transform.rotation);
			}
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
			this.DecreaseHealth(1);
		}
	}

	private char ChooseGender()
	{
		char rndGender;

		int rnd = Random.Range(0, 2);
		if(rnd == 0)
			rndGender = 'M';
		else
			rndGender = 'F';

		return rndGender;
	}
}
