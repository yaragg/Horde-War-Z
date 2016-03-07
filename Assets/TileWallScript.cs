using UnityEngine;
using System.Collections;

public class TileWallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called when object enters this trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        string wallName = this.name;

        //if enemy, halts its movement by modifying the AIMovement script
        if (other.tag == "Enemy")
        {
            switch (wallName)
            {
                case "colliderX+":
                    other.GetComponent<AIMovement>().moveXpos = false;
                    break;
                case "colliderX-":
                    other.GetComponent<AIMovement>().moveXneg = false;
                    break;
                case "colliderY+":
                    other.GetComponent<AIMovement>().moveYpos = false;
                    break;
                case "colliderY-":
                    other.GetComponent<AIMovement>().moveYneg = false;
                    break;
            }
        }

        //if player, halts its movement by modifying the Player script
        if (other.tag == "Player")
        {
            switch (wallName)
            {
                case "colliderX+":
                    other.transform.GetComponentInParent<Player>().moveXpos = false;
                    break;
                case "colliderX-":
                    other.transform.GetComponentInParent<Player>().moveXneg = false;
                    break;
                case "colliderY+":
                    other.transform.GetComponentInParent<Player>().moveYpos = false;
                    break;
                case "colliderY-":
                    other.transform.GetComponentInParent<Player>().moveYneg = false;
                    break;
            }
        }
    }

    // Called when object exits this trigger
    void OnTriggerExit2D(Collider2D other)
    {
        string wallName = this.name;
        //if enemy, resumes its movement by modifying the AIMovement script
        if (other.tag == "Enemy")
        {
            switch (wallName)
            {
                case "colliderX+":
                    other.GetComponent<AIMovement>().moveXpos = true;
                    break;
                case "colliderX-":
                    other.GetComponent<AIMovement>().moveXneg = true;
                    break;
                case "colliderY+":
                    other.GetComponent<AIMovement>().moveYpos = true;
                    break;
                case "colliderY-":
                    other.GetComponent<AIMovement>().moveYneg = true;
                    break;
            }
        }
        //if player, resumes its movement by modifying the Player script
        if (other.tag == "Player")
        {
            switch (wallName)
            {
                case "colliderX+":
                    other.transform.GetComponentInParent<Player>().moveXpos = true;
                    break;
                case "colliderX-":
                    other.transform.GetComponentInParent<Player>().moveXneg = true;
                    break;
                case "colliderY+":
                    other.transform.GetComponentInParent<Player>().moveYpos = true;
                    break;
                case "colliderY-":
                    other.transform.GetComponentInParent<Player>().moveYneg = true;
                    break;
            }
        }
    }
}
