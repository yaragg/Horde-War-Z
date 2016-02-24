using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject bulletType;
    public float fireRate = 10f;
    float lastFired;

	// Use this for initialization
	void Start () {
        lastFired = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(){
        if (Time.deltaTime > fireRate){
            GameObject bullet = (GameObject)Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(transform.forward, -transform.up));
            lastFired = Time.deltaTime;
        }
		//TODO change to transform.forward and transform.up once the forward vector is set properly
	}
}
