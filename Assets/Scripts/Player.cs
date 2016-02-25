using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] characters;
	public float moveSpeed = 7.5f;
    public GameObject healthbarType;
    public GameObject gunType;
	int characterCount = 4;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < 4; i++)
        {
            GameObject go = this.gameObject.transform.GetChild(i).gameObject;
            // Rotates each character's gun so so it fires in the correct direction
            switch (i)
            {
                case 0:
                    go.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.right, -Vector3.forward);
                    break;
                case 1:
                    go.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up, -Vector3.forward);
                    break;
                case 2:
                    go.transform.rotation = Quaternion.LookRotation(transform.position - Vector3.right, -Vector3.forward);
                    break;
                case 3:
                    go.transform.rotation = Quaternion.LookRotation(transform.position - Vector3.up, -Vector3.forward);
                    break;
                default:
                    break;
            }
            GameObject healthbar = (GameObject) Instantiate(healthbarType, new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z+2), Quaternion.identity);
            healthbar.transform.parent = go.transform;

        }

    }
	
	// Update is called once per frame
	void Update () {
		// Move the player
		var h = Input.GetAxis ("Horizontal");
		var v = Input.GetAxis ("Vertical");
		this.transform.Translate (new Vector3 (h, v, 0) * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < 4; i++)
            {
            	GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                if(child.activeSelf) child.transform.GetChild(0).GetComponent<Gun>().Shoot();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.C)){
        	this.transform.Rotate(new Vector3(0, 0, 45));
        	foreach (GameObject character in characters){
				character.transform.GetChild(1).transform.Rotate(0,0,-45);
        	}
			
        }
		if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.V)){
			this.transform.Rotate(new Vector3(0, 0, -45));
			foreach (GameObject character in characters){
				character.transform.GetChild(1).transform.Rotate(0,0,45);
			}
		}
	}

	public void decreaseCharacterCount(){
		characterCount--;

		if(characterCount <= 0)
			GameObject.Find("gameScriptHolder").GetComponent<GameScript>().onGameEnd();
	}
}
