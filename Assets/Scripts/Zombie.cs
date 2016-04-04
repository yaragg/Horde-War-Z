using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public int health = 2;
	public int healthMax = 2;
    public GameObject nameBoxType;

    public int score = 5;
	public int damage = 1;

	public bool alive = true;
	float deathTime;
	public float remainsTimer = 2.0f;

	public GameObject itemDrop;
	public float dropChance = 25.0f;

	public char genderChar = 'M';

	public Sprites spriteHolder;

	// Use this for initialization
	void Start () {

		genderChar = ChooseGender();

        GameObject nameBox = (GameObject)Instantiate(nameBoxType, new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
        nameBox.GetComponent<TextMesh>().text = NameScript.GetName(this.tag, genderChar);
        nameBox.transform.parent = this.transform;

		spriteHolder = GameObject.Find("SpritesHolder").GetComponent<Sprites>();

		SpriteRenderer thisSprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
		thisSprite.sprite = spriteHolder.GetZombieSprite(genderChar);

		// Determine type of zombie, and set stats accordingly
		// Female Zombie Brute
		if (thisSprite.sprite == spriteHolder.spriteZombiesF[0]){
			this.transform.GetComponent<AIMovement>().moveSpeed = Random.Range(2.0f, 2.6f);
			health = 4;
			damage = 2;
			score = 10;
			dropChance = 35.0f;
		}
		// Crawling Zombie
		else if (thisSprite.sprite == spriteHolder.spriteZombiesF[0] || thisSprite.sprite == spriteHolder.spriteZombiesM[2] )
		{
			this.transform.GetComponent<AIMovement>().moveSpeed = Random.Range(0.5f, 1.6f);
			health = 2;
			damage = 1;
			score = 5;
			dropChance = 15.0f;
		}
		// Male Zombie Brute
		else if (thisSprite.sprite == spriteHolder.spriteZombiesM[0]){
			this.transform.GetComponent<AIMovement>().moveSpeed = Random.Range(2.5f, 2.9f);
			health = 2;
			damage = 2;
			score = 10;
			dropChance = 35.0f;
		}
		// Fast Zombie
		else if (thisSprite.sprite == spriteHolder.spriteZombiesM[1]){
			this.transform.GetComponent<AIMovement>().moveSpeed = Random.Range(2.8f, 3.5f);
			health = 3;
			damage = 1;
			score = 8;
			dropChance = 15.0f;
		}

		thisSprite.color = new Color(Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f));
    }
	
	// Update is called once per frame
	void Update () {
		if (!alive && Time.timeSinceLevelLoad - deathTime > remainsTimer)
		{
			if (Random.Range (0.0f, 100.0f) <= dropChance) {
				GameObject.Instantiate(itemDrop, transform.position, transform.rotation);
			}
			Destroy(this.gameObject);
		}
	}

    void OnDestroy()
    {
        GameObject thisName = this.gameObject.transform.GetChild(1).gameObject;
        NameScript.ReturnName(this.tag, genderChar, thisName.GetComponent<TextMesh>().text);
    }

	public void DecreaseHealth(int damage){
		if(health > damage){
			health -= damage;
		}
		else{
			alive = false;
			this.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
			deathTime = Time.timeSinceLevelLoad;
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
		if (col.gameObject.tag == "Player" && alive) {
			col.gameObject.GetComponent<Character>().DecreaseHealth(this.damage);
			Debug.Log(this.damage);
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
