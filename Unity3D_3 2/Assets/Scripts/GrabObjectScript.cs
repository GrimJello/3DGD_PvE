using UnityEngine;
using System.Collections;

public class GrabObjectScript : MonoBehaviour {

	public LayerMask placeLayer;

	private Transform carryLocation; //Stores carry marker gameobject

	private string dropAndPlaceButton;

	private bool holdingObject = false;

	private Rigidbody rb;

	private InteractRaycast interactRaycast;

	private RigidbodyConstraints startConstraints;

	private CameraController camControl;

	private Vector3 startScale;
	private bool canDrop;

	void Start() {
		camControl = GetComponentInChildren<CameraController> ();
		rb = GetComponent<Rigidbody> ();

		startConstraints = rb.constraints;
		startScale = transform.localScale;
	}

	public void Update() {
		
		if (interactRaycast != null) {

			if (Input.GetButtonUp(dropAndPlaceButton)) {
				canDrop = true;
			}

			if(Input.GetButtonDown (dropAndPlaceButton) && canDrop) {
				
				Ray placeRay = new Ray (interactRaycast.gameObject.transform.position, interactRaycast.gameObject.transform.TransformDirection(Vector3.forward));// creates ray for placing camera

				RaycastHit placeHit;

				if (Physics.Raycast (placeRay, out placeHit, 3f, placeLayer)) {

					PlaceObject (placeHit);

					//PlaceObject (placeHit);
				} else {

					ReleaseObject ();
				}

			}

		}

		if (interactRaycast != null) {
			transform.position = carryLocation.position;
			transform.LookAt(interactRaycast.transform,Vector3.up);
		}
	}

	public void GrabObject(GameObject grabbingObject) {
		interactRaycast = grabbingObject.GetComponent<InteractRaycast> ();
		interactRaycast.canInteract = false;
		carryLocation = grabbingObject.transform.parent.FindChild("CarryMarker");

		if (grabbingObject.GetComponentInParent<PlayerManager> ().player == Player.player1) {
			dropAndPlaceButton = "InteractP1";
		} else if (grabbingObject.GetComponentInParent<PlayerManager> ().player == Player.player2) {
			dropAndPlaceButton = "InteractP2";
		} else {
			Debug.Log ("this should not have happened: noPlayer called this objects GrabObject()");
		}

		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		transform.localScale = startScale / 3.0f;
		transform.SetParent (grabbingObject.transform);
		canDrop = false;

	}

	public void ReleaseObject() {

		Debug.Log ("dropped");
		//reset everything to normal
		StartCoroutine("ResetStuff");


		//set transform to nothing
		// -> done
	}

	public void PlaceObject(RaycastHit hit) {
		Debug.Log(LayerMask.NameToLayer("Mountable"));
		Debug.Log (placeLayer.value);

		//reset everything to normal
		if (placeLayer.value == Mathf.Pow(2, LayerMask.NameToLayer ("Mountable"))) {
			StartCoroutine ("SetObjectToWall", hit);
		} else if (placeLayer.value == Mathf.Pow(2, LayerMask.NameToLayer ("Camera"))) {
			StartCoroutine ("RefillAmmoAndDestroy", hit);
		
		}

		//set transform to raycast location if it hit something

	}
	IEnumerator SetObjectToWall(RaycastHit hit) {
		yield return new WaitForEndOfFrame ();
		carryLocation = null;
		rb.useGravity = false; //because it should not fall off the wall
		transform.position = new Vector3 (hit.point.x, hit.point.y, hit.point.z);


		rb.constraints = RigidbodyConstraints.FreezeAll;
		transform.localScale = startScale;
		transform.SetParent (GameObject.FindGameObjectWithTag ("Level").transform);
		transform.forward = hit.normal;
		camControl.SetSurfaceNormal (hit.normal);
		transform.Translate (new Vector3 (0, 0, 1));
		dropAndPlaceButton = null;
		interactRaycast.canInteract = true;
		interactRaycast = null;

	}

	IEnumerator RefillAmmoAndDestroy(RaycastHit hit) {
		yield return new WaitForEndOfFrame ();
		hit.collider.gameObject.GetComponentInChildren<CamShootingManager> ().ReloadWeapon ();
		interactRaycast.canInteract = true;
		Destroy (gameObject);
	}

	IEnumerator ResetStuff() {
		yield return new WaitForEndOfFrame ();
		carryLocation = null;

		rb.useGravity = true;
		rb.constraints = startConstraints;
		transform.localScale = startScale;
		transform.SetParent (GameObject.FindGameObjectWithTag ("Level").transform);
		dropAndPlaceButton = null;
		interactRaycast.canInteract = true;
		interactRaycast = null;

	}

}
