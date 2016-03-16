using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    public float moveSpeed = 2.5f;
	public float maxDist = 8.0f;
	public float hoardDist = 2.0f;
	public int hoardMembers = 0;
	GameObject player;
	GameObject[] zombies;

    public bool moveXpos = true;
    public bool moveXneg = true;
    public bool moveYpos = true;
    public bool moveYneg = true;

	public Vector3 velocity;

	public  float seekWt = 1.5f;
	public  float seperationWt = 1.5f;
	public  float cohesionWt = 1f;
	public  float alignmentWt = 1f;

	// Use this for initialization
	void Start () {
		GameObject[] chars = GameObject.FindGameObjectsWithTag("Player");
		player = chars [Random.Range (0, 3)];
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.gameObject.GetComponent<Zombie> ().alive) { moveSpeed = 0; }
		zombies = GameObject.FindGameObjectsWithTag ("Enemy");
        Vector3 currPos = this.transform.position;
        Vector3 targetPos = player.transform.position;

		// Creates a hoard like behavior
		Vector3 alignment = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 speration = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 cohesion = new Vector3(0.0f, 0.0f, 0.0f);
		hoardMembers = 0;
		foreach (GameObject zom in zombies) {
			if (Vector3.Distance(currPos, zom.transform.position) < hoardDist && zom.GetComponent<Zombie>().alive){
				hoardMembers++;
				alignment.x += zom.GetComponent<AIMovement>().velocity.x;
				alignment.y += zom.GetComponent<AIMovement>().velocity.y;
				cohesion.x += zom.transform.position.x;
				cohesion.y += zom.transform.position.y;
				speration.x += currPos.x - zom.transform.position.x;
				speration.y += currPos.y - zom.transform.position.y;
			}
		}

		// Calculate final movement vector
		if (hoardMembers > 0) {
			alignment.x /= hoardMembers;
			alignment.y /= hoardMembers;
			cohesion.x /= hoardMembers;
			cohesion.y /= hoardMembers;
			cohesion = new Vector3 (cohesion.x - currPos.x, cohesion.y - currPos.y, 0.0f);

			velocity.x += ((targetPos.x - currPos.x) * seekWt) + (alignment.normalized.x * alignmentWt) + (cohesion.normalized.x * cohesionWt) + (speration.normalized.x * seperationWt);
			velocity.y += ((targetPos.y - currPos.y) * seekWt) + (alignment.normalized.y * alignmentWt) + (cohesion.normalized.y * cohesionWt) + (speration.normalized.y * seperationWt);
		} else {
			velocity.x += ((targetPos.x - currPos.x) * seekWt);
			velocity.y += ((targetPos.y - currPos.y) * seekWt);
		}

		velocity.Normalize();
		velocity *= moveSpeed;

		// Wall Collisions
		if(!moveXpos)
		{
			if (targetPos.x > currPos.x)
				velocity.x -= moveSpeed;
		}
		if (!moveXneg)
		{
			if (targetPos.x < currPos.x)
				velocity.x += moveSpeed;
		}
		if (!moveYpos)
		{
			if (targetPos.y > currPos.y)
				velocity.y -= moveSpeed;
		}
		if (!moveYneg)
		{
			if (targetPos.y < currPos.y)
				velocity.y += moveSpeed;
		}

        this.transform.Translate(velocity * Time.deltaTime);
	}

    // METHOD FOR FIXING OBJECTS ESCAPING THROUGH WALLS PROBLEM
    // LateUpdate is called every frame, immediately after Update
    /*void LateUpdate()
    {
        moveXpos = true;
        moveXneg = true;
        moveYpos = true;
        moveYneg = true;
    }*/


}
