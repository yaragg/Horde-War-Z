using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public int health = 2;
	public int healthMax = 2;
    public GameObject nameBoxType;

    public int score = 5;

	// Use this for initialization
	void Start () {

        GameObject nameBox = (GameObject)Instantiate(nameBoxType, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        nameBox.GetComponent<TextMesh>().text = NameScript.GetName(this.tag);
        nameBox.transform.parent = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GameObject thisName = this.gameObject.transform.GetChild(0).gameObject;
        NameScript.ReturnName(this.tag, thisName.GetComponent<TextMesh>().text);
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
