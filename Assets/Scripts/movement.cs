using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        this.transform.Translate(new Vector3(h , v, 0) * moveSpeed * Time.deltaTime);
	}
}
