using UnityEngine;
using System.Collections;

public class sightTest : MonoBehaviour {



	public Transform target;


	

	void Update () {
		
		Debug.DrawRay (transform.position, target.position - transform.position);

		Ray sightRay = new Ray (transform.position, target.position - transform.position);
		RaycastHit hit;

		if(Physics.Raycast( sightRay, out hit)){
			Debug.Log (hit.collider);
		}

	}
}
