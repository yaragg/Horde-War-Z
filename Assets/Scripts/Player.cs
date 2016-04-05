using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List<GameObject> characters;
	public float moveSpeed = 7.5f;
    public GameObject healthbarType;
    public GameObject nameBoxType;
    public GameObject gunType;
    public AudioClip emergencyBGM;
	public int characterCount = 4;
    int currentFormation = 0, maxFormations = 2;
	float rotateTime;
	public float rotateDelay = 0.01f;

	GameObject camera;
	public int camThresholdX = 2;
	public int camThresholdY = 2;
	public float smoothness = 0.25f;

    public bool moveXpos = true;
    public bool moveXneg = true;
    public bool moveYpos = true;
    public bool moveYneg = true;

    // Use this for initialization
    void Start() {
		rotateTime = Time.timeSinceLevelLoad;
        characters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < 4; i++)
        {
            GameObject go = characters[i];
            // Rotates each character so it fires in the correct direction
            switch (i)
            {
                case 0:
					go.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.right, -Vector3.forward);
					go.transform.GetChild(0).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(0).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(1).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.position += go.transform.forward * 0.65f;
					break;
                case 1:
                    go.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up, -Vector3.forward);
					go.transform.GetChild(0).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(0).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(1).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.position += go.transform.forward * 0.8f;
                    break;
                case 2:
                    go.transform.rotation = Quaternion.LookRotation(transform.position - Vector3.right, -Vector3.forward);
					go.transform.GetChild(0).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(0).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(1).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.position += go.transform.forward * 0.75f;
                    break;
                case 3:
                    go.transform.rotation = Quaternion.LookRotation(transform.position - Vector3.up, -Vector3.forward);
					go.transform.GetChild(0).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(0).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.rotation = Quaternion.LookRotation(go.transform.GetChild(1).transform.up - ( 0.3f * go.transform.forward), -Vector3.forward);
					go.transform.GetChild(1).transform.position += go.transform.forward * 0.95f;
                    break;
                default:
                    break;
            }
            GameObject healthbar = (GameObject)Instantiate(healthbarType, new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z +5), Quaternion.identity);
            healthbar.transform.parent = go.transform;

			GameObject nameBox = (GameObject)Instantiate(nameBoxType, new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z +5), Quaternion.identity);
            nameBox.GetComponent<TextMesh>().text = NameScript.Character_Names[i];
            nameBox.transform.parent = go.transform;

        }
        DiamondFormation();

		camera = GameObject.Find ("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
		// Move the player
		var h = Input.GetAxis ("Horizontal");
		var v = Input.GetAxis ("Vertical");

        Vector3 moveVector = new Vector3(h, v, 0);

        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < 4; i++)
            {
            	GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                if(child.activeSelf) child.transform.GetChild(1).GetComponent<Gun>().Shoot();
            }
        }

        if(Input.GetMouseButtonDown(1)){
            currentFormation = (currentFormation+1)%maxFormations;
            PickFormation(currentFormation);
        }

        if(Input.GetMouseButtonDown(2)){
        	PickFormation(currentFormation);
        }
            
        
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.C)){
			transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)), Time.fixedDeltaTime * 1.5f);
    		foreach (GameObject character in characters){
				Quaternion rot = character.transform.GetChild(2).transform.rotation;
				character.transform.GetChild(2).transform.rotation = Quaternion.Lerp(rot, rot * Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f)), Time.fixedDeltaTime * 1.5f);
				character.transform.GetChild(3).transform.rotation = Quaternion.Lerp(rot, rot * Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f)), Time.fixedDeltaTime * 1.5f);
        	}
        }
		if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.V)){
			transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f)), Time.fixedDeltaTime * 1.5f);
			foreach (GameObject character in characters){
				Quaternion rot = character.transform.GetChild(2).transform.rotation;
				character.transform.GetChild(2).transform.rotation = Quaternion.Lerp(rot, rot * Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)), Time.fixedDeltaTime * 1.5f);
				character.transform.GetChild(3).transform.rotation = Quaternion.Lerp(rot, rot * Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)), Time.fixedDeltaTime * 1.5f);
			}
		}

		if (!moveXpos)
		{
			if (h > 0)
				moveVector.x = 0;
		}
		if (!moveXneg)
		{
			if (h < 0)
				moveVector.x = 0;
		}
		if (!moveYpos)
		{
			if (v > 0)
				moveVector.y = 0;
		}
		if (!moveYneg)
		{
			if (v < 0)
				moveVector.y = 0;
		}

		this.transform.position = Vector3.Lerp(transform.position, transform.position + (moveVector * moveSpeed), Time.deltaTime);
		moveCamera(camera);
	}

    void PickFormation(int num){
        switch(num)
            {
                case 0:
                    DiamondFormation();
                    break;
                case 1:
                    LineFormation();
                    break;
                default:
                    break;
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


    void DiamondFormation(){
            // Rotates each character's gun so so it fires in the correct direction
            for (int i = 0; i < 4; i++)
            {
                GameObject go = characters[i];
                GameObject healthbar = go.transform.GetChild(2).gameObject;
                GameObject nameBox = go.transform.GetChild(3).gameObject;

                healthbar.transform.parent = null;
                nameBox.transform.parent = null;

                switch (i)
                {
                    case 0:
                    	go.transform.localPosition = new Vector3(1, 0, 0);
                        go.transform.rotation = Quaternion.LookRotation(Vector3.right, -Vector3.forward);
                        go.transform.rotation = Quaternion.LookRotation(go.transform.parent.right, -go.transform.parent.forward);
                        break;
                    case 1:
                    	go.transform.localPosition = new Vector3(0, 1, 0);
                        go.transform.rotation = Quaternion.LookRotation(Vector3.up, -Vector3.forward);
                        go.transform.rotation = Quaternion.LookRotation(go.transform.parent.up, -go.transform.parent.forward);
                        break;
                    case 2:
                    	go.transform.localPosition = new Vector3(-1, 0, 0);
                        go.transform.rotation = Quaternion.LookRotation(-Vector3.right, -Vector3.forward);
                        go.transform.rotation = Quaternion.LookRotation(-go.transform.parent.right, -go.transform.parent.forward);

                        break;
                    case 3:
                    	go.transform.localPosition = new Vector3(0, -1, 0);
                        go.transform.rotation = Quaternion.LookRotation(-Vector3.up, -Vector3.forward);
                        go.transform.rotation = Quaternion.LookRotation(-go.transform.parent.up, -go.transform.parent.forward);
                        break;
                    default:
                        break;
                }
                healthbar.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
                healthbar.transform.parent = go.transform;

                nameBox.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
                nameBox.transform.parent = go.transform;
        }
    }

    void LineFormation(){
    	int i=0;
    	if(characterCount == 4){
    		foreach (GameObject go in characters)
            {
                if(!go.activeSelf) continue;
                GameObject healthbar = go.transform.GetChild(2).gameObject;
                GameObject nameBox = go.transform.GetChild(3).gameObject;

                healthbar.transform.parent = null;
                nameBox.transform.parent = null;

                switch (i)
                {
                    case 0:
                        go.transform.localPosition = new Vector3(-0.7f, 0, 0);
                        break;
                    case 1:
                        go.transform.localPosition = new Vector3(0.7f, 0, 0);
                        break;
                    case 2:
                        go.transform.localPosition = new Vector3(1.7f, 0, 0);
                        break;
                    case 3:
                    	go.transform.localPosition = new Vector3(-1.7f, 0, 0);
                        break;
                    default:
                        break;
                }
                i++;
                go.transform.rotation = Quaternion.LookRotation(go.transform.parent.up, -go.transform.parent.forward);
                healthbar.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z+2);
                healthbar.transform.parent = go.transform;

                nameBox.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
                nameBox.transform.parent = go.transform;
        	}
    	}
    	else if(characterCount == 3){
    		foreach (GameObject go in characters)
            {
                if(!go.activeSelf) continue;
                GameObject healthbar = go.transform.GetChild(2).gameObject;
                GameObject nameBox = go.transform.GetChild(3).gameObject;

                healthbar.transform.parent = null;
                nameBox.transform.parent = null;

                switch (i)
                {
                    case 0:
                        go.transform.localPosition = new Vector3(-1.2f, 0, 0);
                        break;
                    case 1:
                        go.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    case 2:
                    	go.transform.localPosition = new Vector3(1.2f, 0, 0);
                        break;
                    default:
                        break;
                }
                i++;
                go.transform.rotation = Quaternion.LookRotation(go.transform.parent.up, -go.transform.parent.forward);
                healthbar.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z+2);
                healthbar.transform.parent = go.transform;

                nameBox.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
                nameBox.transform.parent = go.transform;
        	}
    	}
    	else if(characterCount == 2){
    		foreach (GameObject go in characters)
            {
                if(!go.activeSelf) continue;
                GameObject healthbar = go.transform.GetChild(2).gameObject;
                GameObject nameBox = go.transform.GetChild(3).gameObject;

                healthbar.transform.parent = null;
                nameBox.transform.parent = null;

                switch (i)
                {
                    case 0:
                        go.transform.localPosition = new Vector3(-0.5f, 0, 0);
                        break;
                    case 1:
                        go.transform.localPosition = new Vector3(0.5f, 0, 0);
                        break;
                    default:
                        break;
                }
                i++;
                go.transform.rotation = Quaternion.LookRotation(go.transform.parent.up, -go.transform.parent.forward);
                healthbar.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z+2);
                healthbar.transform.parent = go.transform;

                nameBox.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
                nameBox.transform.parent = go.transform;
        	}
        }
    	else if(characterCount == 1){
    		foreach (GameObject go in characters)
    		{
	            if(!go.activeSelf) continue;
	            GameObject healthbar = go.transform.GetChild(2).gameObject;
	            GameObject nameBox = go.transform.GetChild(3).gameObject;

	            healthbar.transform.parent = null;
	            nameBox.transform.parent = null;

	            go.transform.localPosition = new Vector3(0, 0, 0);

	            go.transform.rotation = Quaternion.LookRotation(go.transform.parent.up, -go.transform.parent.forward);
	            healthbar.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
	            healthbar.transform.parent = go.transform;

	            nameBox.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 2);
	            nameBox.transform.parent = go.transform;
	            break;
    		}
            
		}
            
    }

	public void decreaseCharacterCount(){
		characterCount--;
		PickFormation(currentFormation);

        if(characterCount == 2){
            AudioSource audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            audioSource.clip = emergencyBGM;
            audioSource.Play();
        }

		if(characterCount <= 0) {
			GameObject.Find("gameScriptHolder").GetComponent<GameScriptScore>().onGameEnd();
		}
		//PickFormation(currentFormation);
	}

	void moveCamera(GameObject cam){
		Vector3 camDifference = this.transform.position - cam.transform.position;
		camDifference.z = 0;
		if (camDifference.x >= camThresholdX) {
			cam.transform.Translate(camDifference * smoothness);
		}
		else if (camDifference.x <= -camThresholdX) {
			cam.transform.Translate(camDifference * smoothness);
		}
		if (camDifference.y >= camThresholdY) {
			cam.transform.Translate(camDifference * smoothness);
		}
		else if (camDifference.y <= -camThresholdY) {
			cam.transform.Translate(camDifference * smoothness);
		}
	}
}
