using UnityEngine;
using System.Collections;

public class GrabRaycast : MonoBehaviour
{

	public float rayLength = 10.0f;
	public LayerMask grabLayer;

	private GameObject cameraGO;
	private Rigidbody cameraRb;

	private bool grabbing = false;

	
	// Update is called once per frame
	void Update ()
	{
		//Shoots ray from player forward the amount of the rayLenght variable
		Ray grabRay = new Ray (transform.position, transform.forward * rayLength);

		RaycastHit grabHit;
		Debug.DrawRay (transform.position, transform.forward * rayLength, Color.magenta);

	
		if (Input.GetButtonDown ("Fire1")) {

			if (!grabbing) {
				if (Physics.Raycast (grabRay, out grabHit, grabLayer)) {
					Debug.Log ("grab");

					grabbing = true;

					cameraGO = grabHit.collider.gameObject;
					cameraRb = cameraGO.GetComponent<Rigidbody> ();
					cameraRb.useGravity = false;
					cameraRb.constraints = RigidbodyConstraints.FreezeRotation;



				}
			} else if (grabbing) {
				Debug.Log ("drop");

				grabbing = false;

				cameraRb.useGravity = true;
				cameraGO.transform.parent = null;
				cameraRb.constraints = RigidbodyConstraints.None;
			}
		}
	}

	void FixedUpdate(){

		if (grabbing) {


			Vector3 newGOPosition = cameraGO.transform.position + Camera.main.transform.forward;
		
			cameraGO.transform.position = newGOPosition;
			cameraRb.velocity = Vector3.zero;



		}
	}
}
			
	