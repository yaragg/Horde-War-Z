using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] characters;
	public float moveSpeed = 7.5f;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < 4; i++)
        {
            //TODO rotate all to have same "UP"
            switch (i)
            {
                case 0:
                    this.gameObject.transform.GetChild(i).forward = transform.right * -1;
                    break;
                case 1:
                    this.gameObject.transform.GetChild(i).forward = transform.up;
                    break;
                case 2:
                    this.gameObject.transform.GetChild(i).forward = transform.right;
                    break;
                case 3:
                    this.gameObject.transform.GetChild(i).forward = transform.up * -1;
                    break;
                default:
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		// Move the player
		var h = Input.GetAxis ("Horizontal");
		var v = Input.GetAxis ("Vertical");
		this.transform.Translate (new Vector3 (h, v, 0) * moveSpeed * Time.deltaTime);

		// Move all characters to updated position relative to the player
		characters [0].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y - characters [0].transform.localScale.y, 0 );
		characters [1].transform.localPosition = new Vector3 (this.transform.position.x + characters [1].transform.localScale.x, this.transform.position.y, 0 );
		characters [2].transform.localPosition = new Vector3 (this.transform.position.x, this.transform.position.y + characters [2].transform.localScale.y, 0 );
		characters [3].transform.localPosition = new Vector3 (this.transform.position.x  - characters [3].transform.localScale.x, this.transform.position.y, 0 );

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 4; i++)
            {
                this.gameObject.transform.GetChild(i).GetChild(1).GetComponent<Gun>().Shoot();
            }
        }
    }
}
