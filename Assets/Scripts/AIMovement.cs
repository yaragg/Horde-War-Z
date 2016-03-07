using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    public float moveSpeed = 6.0f;
	public float maxDist = 8.0f;
    GameObject player;

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
        Vector3 currPos = this.transform.position;
        Vector3 targetPos = player.transform.position;
	    Vector3 toPlayer = new Vector3();
		//if (!(toPlayer.magnitude > maxDist)) {
		toPlayer = targetPos - currPos;
			
		//}

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

        this.transform.Translate(toPlayer.normalized * moveSpeed * Time.deltaTime);
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
