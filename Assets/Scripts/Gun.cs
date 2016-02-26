using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject bulletType;
    
	public enum weaponType{pistol, sniper, smg, shotgun};
	public weaponType gunType;

	// Fire Rate
	float fireRate = 10f;
    float lastFired;

	// Use this for initialization
	void Start () {
		lastFired = Time.frameCount;
	}
	
	// Update is called once per frame
	void Update () {
		switch (gunType) {
		case weaponType.pistol: fireRate = 12f;
			break;
		case weaponType.shotgun: fireRate = 15f;
			break;
		case weaponType.smg: fireRate = 5f;
			break;
		case weaponType.sniper: fireRate = 25f;
			break;
		}
	}

	public void Shoot(){
		switch (gunType) {
			case weaponType.pistol:
			if (Time.frameCount - lastFired > fireRate){
				Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(transform.forward, -transform.up));
				lastFired = Time.frameCount;
			}
			break;
			case weaponType.shotgun:
			if (Time.frameCount - lastFired > fireRate){
				Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(transform.forward, -transform.up));
				Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(Quaternion.AngleAxis(-15, -transform.up) * transform.forward, -transform.up));
				Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(Quaternion.AngleAxis(15, -transform.up) * transform.forward, -transform.up));
				lastFired = Time.frameCount;
			}
			break;
			case weaponType.smg:
			if (Time.frameCount - lastFired > fireRate){
				Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(transform.forward, -transform.up));
				lastFired = Time.frameCount;
			}
			break;
			case weaponType.sniper:
			if (Time.frameCount - lastFired > fireRate){
				Instantiate(bulletType, this.transform.position, Quaternion.LookRotation(transform.forward, -transform.up));
				lastFired = Time.frameCount;
			}
			break;
		}
		//TODO change to transform.forward and transform.up once the forward vector is set properly
	}
}
