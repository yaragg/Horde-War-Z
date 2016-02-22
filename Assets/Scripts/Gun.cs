using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject bulletType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(){
		Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(transform.up, transform.right));
		//TODO change to transform.forward and transform.up once the forward vector is set properly
	}
}
