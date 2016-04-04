using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    public float moveSpeed = 2.5f;
	public float maxDist = 8.0f;
	public float avoidDist = 8.0f;
    GameObject player;
	GameObject[] zombies;

    public bool moveXpos = true;
    public bool moveXneg = true;
    public bool moveYpos = true;
    public bool moveYneg = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		zombies = GameObject.FindGameObjectsWithTag ("Enemy");
        Vector3 currPos = this.transform.position;
        Vector3 targetPos = player.transform.position;
	    Vector3 toPlayer = new Vector3();
		// Creates a vector pointing away from nearby zombies
		Vector3 zomAway = new Vector3(0.0f, 0.0f, 0.0f);
		foreach (GameObject zom in zombies) {
			if ((currPos - zom.transform.position).magnitude < avoidDist && this.gameObject != zom){
				zomAway += zom.transform.position - currPos;
			}
		}
		// Calculate final movement vector
		toPlayer = targetPos - currPos;
		toPlayer -= zomAway * 4.0f;

		// Wall Collisions
        if(!moveXpos)
        {
            if (targetPos.x > currPos.x)
				toPlayer.x = 0;
        }
        if (!moveXneg)
        {
            if (targetPos.x < currPos.x)
				toPlayer.x = 0;
        }
        if (!moveYpos)
        {
            if (targetPos.y > currPos.y)
				toPlayer.y = 0;
        }
        if (!moveYneg)
        {
            if (targetPos.y < currPos.y)
				toPlayer.y = 0;
        }
		
        if (transform.GetComponent<Zombie>().alive) 
		{
			float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
			angle -= 90.0f;
			this.transform.GetChild(0).transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			this.transform.Translate(toPlayer.normalized * moveSpeed * Time.deltaTime);
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
}
