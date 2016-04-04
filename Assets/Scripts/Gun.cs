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
		case weaponType.pistol: fireRate = 18f;
			break;
		case weaponType.shotgun: fireRate = 30f;
			break;
		case weaponType.smg: fireRate = 5f;
			break;
		case weaponType.sniper: fireRate = 50f;
			break;
		}
	}

	public void Shoot(){
		switch (gunType) {
			case weaponType.pistol:
			    if (Time.frameCount - lastFired > fireRate){
				GameObject bullet = (GameObject)Instantiate(bulletType, this.transform.position + (transform.parent.transform.forward * 0.22f), Quaternion.LookRotation(-transform.parent.transform.forward, -Vector3.forward));
                    bullet.GetComponent<Bullet>().damage = 2;
                    bullet.GetComponent<Bullet>().lifespan = 1;
				    lastFired = Time.frameCount;
			    }
			break;
			case weaponType.shotgun:
			    if (Time.frameCount - lastFired > fireRate){
				GameObject bullet = (GameObject) Instantiate(bulletType, this.transform.position + (transform.parent.transform.forward * 0.4f), Quaternion.LookRotation(-transform.parent.transform.forward, -Vector3.forward));
				    bullet.GetComponent<Bullet>().damage = 4;
                    bullet.GetComponent<Bullet>().lifespan = 0.4f;
					bullet = (GameObject) Instantiate(bulletType, this.transform.position + (transform.parent.transform.forward * 0.4f), Quaternion.LookRotation(Quaternion.AngleAxis(-10, -Vector3.forward) * -transform.parent.transform.forward, -Vector3.forward));
				    bullet.GetComponent<Bullet>().damage = 4;
                    bullet.GetComponent<Bullet>().lifespan = 0.4f;
					bullet = (GameObject) Instantiate(bulletType, this.transform.position + (transform.parent.transform.forward * 0.4f), Quaternion.LookRotation(Quaternion.AngleAxis(10, -Vector3.forward) * -transform.parent.transform.forward, -Vector3.forward));
				    bullet.GetComponent<Bullet>().damage = 4;
                    bullet.GetComponent<Bullet>().lifespan = 0.4f;
                    lastFired = Time.frameCount;
			    }
			break;
			case weaponType.smg:
			    if (Time.frameCount - lastFired > fireRate){
				GameObject bullet = (GameObject)Instantiate(bulletType, this.transform.position + (transform.parent.transform.forward * 0.38f), Quaternion.LookRotation(-transform.parent.transform.forward, -Vector3.forward));
                    bullet.GetComponent<Bullet>().damage = 1;
                    bullet.GetComponent<Bullet>().lifespan = 0.6f;
				    lastFired = Time.frameCount;
			    }
			break;
			case weaponType.sniper:
			    if (Time.frameCount - lastFired > fireRate){
				GameObject  bullet = (GameObject) Instantiate(bulletType, this.transform.position + (transform.parent.transform.forward * 0.42f), Quaternion.LookRotation(-transform.parent.transform.forward, -Vector3.forward));
				    bullet.GetComponent<Bullet>().damage = 4;
                    bullet.GetComponent<Bullet>().lifespan = 2;
					bullet.GetComponent<Bullet>().persist = true;
				    lastFired = Time.frameCount;
			    }
			break;
		}
	}
}
