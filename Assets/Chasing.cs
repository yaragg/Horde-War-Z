using UnityEngine;
using System.Collections;
public class chasing : MonoBehaviour{

	var Character : Transform;
	public int MaxDist = 10;
	public int MinDist = 5;

	void Start (){
	}

	void Update () 
	{
		transform.LookAt(Character);

		if(Vector3.Distance(transform.position,Character.position) >= MinDist){

			transform.position += transform.forward*MoveSpeed*Time.deltaTime;
			if(Vector3.Distance(transform.position,Character.position) <= MaxDist){

			} 

		}
	}