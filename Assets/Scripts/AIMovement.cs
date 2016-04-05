using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    public float moveSpeed = 2.5f;
	float currSpeed;
	public float maxDist = 8.0f;
	public float avoidDist = 8.0f;
	public float sightDist = 10.0f;

    GameObject player;
	GameObject[] zombies;

	public float seekWt = 5.0f;
	public float avoidWt = 1.0f;
	public float wanderWt = 1.0f;
	Vector3 velocity;
	Vector3 position;
	Vector3 w_Direction;

    public bool moveXpos = true;
    public bool moveXneg = true;
    public bool moveYpos = true;
    public bool moveYneg = true;

	// Use this for initialization
	void Start () {
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		player = GameObject.Find("Player");
		velocity = Vector3.up;
		w_Direction = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {
		zombies = GameObject.FindGameObjectsWithTag ("Enemy");
        position = this.transform.position;
		Vector3 acceleration = velocity;
        Vector3 targetPos = player.transform.position;

		foreach (GameObject zom in zombies) {
			acceleration += (Avoid(zom.transform.position, avoidDist)) * avoidWt;
		}

		// IF player is in sight
		if (Vector3.Magnitude(transform.position - targetPos) < sightDist) {
			acceleration += Seek(targetPos) * seekWt;
			currSpeed = moveSpeed;
		}
		else {
			acceleration += Wander(velocity, w_Direction, 5.0f, 0.1f) * wanderWt;
			currSpeed = moveSpeed / 4f;
		}

		// Calculate final movement vector
		//toPlayer = (targetPos - currPos) * seekWt;
		//toPlayer += zomAway * avoidWt;

		// Wall Collisions
        if(!moveXpos || !moveXneg)
        {
            //if (targetPos.x > currPos.x)
			acceleration.x *= -1.0f;
			w_Direction.x *= -1.0f;
        }
        if (!moveYpos || !moveYneg)
        {
            //if (targetPos.y > currPos.y)
			acceleration.y *= -1.0f;
			w_Direction.y *= -1.0f;
        }
		
        if (transform.GetComponent<Zombie>().alive) 
		{
			float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
			this.transform.GetChild(0).transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			velocity = acceleration.normalized * currSpeed;
			transform.position = Vector3.Lerp(transform.position, transform.position + velocity, Time.deltaTime);;
		}
	}

    //METHOD FOR FIXING OBJECTS ESCAPING THROUGH WALLS PROBLEM
    //// LateUpdate is called every frame, immediately after Update
    //void LateUpdate()
    //{
    //    moveXpos = true;
    //    moveXneg = true;
    //    moveYpos = true;
    //    moveYneg = true;
    //}

	Vector3 Seek(Vector3 location)
	{
		return location - transform.position;
	}

	Vector3 Wander(Vector3 forward, Vector3 wanderDir, float angle, float wanderDist)
	{
		float w_angle = Mathf.Atan2(wanderDir.y, wanderDir.x) * Mathf.Rad2Deg * Random.Range(-angle, angle);
		Vector3 result = Quaternion.AngleAxis(w_angle, Vector3.forward) * (wanderDir.normalized * wanderDist);
		w_Direction = result;
		result += (forward * 20.0f);
		return result;
	}

	Vector3 Avoid(Vector3 position, float dist)
	{
		Vector3 result;
		// Too Close!!
		if (Vector3.Distance(transform.position, position) < dist) {
			result = transform.position - position;
		}
		else
			result = Vector3.zero;
		return result;
	}
}
