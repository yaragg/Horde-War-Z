using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int health = 4;
	public int healthMax = 4;
	public Material full;
	public Material depleted;

	AudioSource audioSource;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DecreaseHealth(int damage){
		if(audioSource){
			if(!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		if(health > damage){
			for (int i = 0; i < damage; i++){
				health--;
				GameObject heart = this.gameObject.transform.GetChild(2).GetChild(health).gameObject;
				Renderer rend = heart.GetComponent<Renderer>();
				rend.enabled = true;
				rend.sharedMaterial = depleted;
			}
		}
		else{
			GameObject.Find("Player").GetComponent<Player>().decreaseCharacterCount();
            GameObject thisName = this.gameObject.transform.GetChild(3).gameObject;
            //NameScript.ReturnName(this.tag, thisName.GetComponent<TextMesh>().text);

            //Destroy(gameObject);
            gameObject.SetActive(false);
		}
	}

	public void IncreaseHealth(){
		if(health < healthMax){
			GameObject heart = this.gameObject.transform.GetChild(2).GetChild(health).gameObject;
			Renderer rend = heart.GetComponent<Renderer>();
			rend.enabled = true;
			rend.sharedMaterial = full;
			health++;
		}
	}

	public void Shoot(){

	}
}
