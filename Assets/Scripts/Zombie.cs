using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public int health = 2;
	public int healthMax = 2;
    public GameObject nameBoxType;
	GameObject nameBox;

    public int score = 5;

	public GameObject itemDrop;
	public float dropChance = 15.0f;

	public bool deceased = false;
	float deathTime = 0;
	public float remainsTimer = 1.0f;

	// Use this for initialization
	void Start () {

        nameBox = (GameObject)Instantiate(nameBoxType, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        nameBox.GetComponent<TextMesh>().text = NameScript.GetName(this.tag);
        nameBox.transform.parent = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = Quaternion.identity;
		if (deceased && Time.timeSinceLevelLoad - deathTime >= remainsTimer) {
			if (Random.Range (0.0f, 100.0f) <= dropChance) {
				GameObject.Instantiate(itemDrop, transform.position, transform.rotation);
			}
			Destroy(this.gameObject);
		}
	}

    void OnDestroy()
    {
        GameObject thisName = this.gameObject.transform.GetChild(0).gameObject;
        NameScript.ReturnName(this.tag, thisName.GetComponent<TextMesh>().text);
    }

	public void DecreaseHealth(int damage){
		if(health > damage){
			health-=damage;
		}
		else{
			deceased = true;
			deathTime = Time.timeSinceLevelLoad;
			nameBox.GetComponent<TextMesh>().color = Color.red;
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
		if (col.gameObject.tag == "Player" && !deceased) {
			col.gameObject.GetComponent<Character>().DecreaseHealth();
			this.DecreaseHealth(1);
		}
	}
}
