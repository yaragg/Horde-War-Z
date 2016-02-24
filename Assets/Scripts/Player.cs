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
		this.transform.Translate (new Vector3 (h, v, 0) * moveSpeed * Time.deltaTime);

		// Move all characters to updated position relative to the player

		// if(characters[0].activeSelf) characters [0].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y - characters [0].transform.localScale.y, 0 );
		// if(characters[1].activeSelf) characters [1].transform.localPosition = new Vector3 (this.transform.position.x + characters [0].transform.localScale.x, this.transform.position.y, 0 );
		// if(characters[2].activeSelf) characters [2].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y + characters [0].transform.localScale.y, 0 );
		// if(characters[3].activeSelf) characters [3].transform.localPosition = new Vector3 (this.transform.position.x  - characters [0].transform.localScale.x, this.transform.position.y, 0 );

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 4; i++)
            {
            	GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                if(child.activeSelf) child.transform.GetChild(0).GetComponent<Gun>().Shoot();
            }
        }
	}

	public void decreaseCharacterCount(){
		characterCount--;

		if(characterCount <= 0)
			GameObject.Find("gameScriptHolder").GetComponent<GameScript>().onGameEnd();
	}
}
