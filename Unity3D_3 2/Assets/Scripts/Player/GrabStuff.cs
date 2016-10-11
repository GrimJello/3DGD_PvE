using UnityEngine;
using System.Collections;

public class GrabStuff : MonoBehaviour
{
	/*
	NOTES: 
	-Rotate the picked up object to always have the camera attached to it facing away from the player
	-Make the held object less jittery when player moves
	-Add fart noise to place camera button if no camera is in the players hand
	*/

	public GameObject carryLocation; //Stores carry marker gameobject
	private Vector3 carryPos; //Stores the Position of carryLocation's Gameobject

	//variables relating to grabbing an object
	private string grabButton;//Reference to input manager
	private string placeButton;//Button to place a camera on the wall and ceiling (maybe)

	public LayerMask grabLayer;//Objects you can grab must be on this layer
	public LayerMask placeLayer;//Layer for what a camera can be placed on
	public float rayLength = 2.0f;//how far away an object can be and still get picked up

	private GameObject otherObject;//The object that the player picks up will be stored here
	private bool holdingObject = false;//Depending on this value the player will be able to pickup or drop objects

	private PlayerManager playerManager;

	void Start() {
		playerManager = GetComponentInParent<PlayerManager>();

		if (playerManager.player == Player.player1) {
			grabButton = "InteractP1";
			placeButton = "BackP1";
		} else if (playerManager.player == Player.player2){
			grabButton = "InteractP2";
			placeButton = "BackP2";
		}
	}

	void Update ()
	{	if (!playerManager.isControllingCam) {
			//Sets the Vector3 carryPos to be equal to the position marked in the inspector by the Carry Marker
			carryPos = carryLocation.transform.position;
	
			if (Input.GetButtonDown (grabButton)) {

				if (!holdingObject) {
					//Creates new ray
					Ray grabRay = new Ray (transform.position, transform.forward * rayLength);
					RaycastHit hit;//Creates raycastHit variable(will be true on hit of correct maskingLayer)
					Debug.DrawRay (transform.position, transform.forward * rayLength);

					if (Physics.Raycast (grabRay, out hit, rayLength, grabLayer)) {
						holdingObject = true;//So you can drop object upon next button press
						otherObject = hit.collider.gameObject; //Sets otherObject to GameObject you are picking up
						otherObject.GetComponent<Rigidbody> ().useGravity = false;//Turns off gravity on the object your picking up
						otherObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;//Stops object from being moved while being held
						otherObject.transform.localScale -= new Vector3 ((otherObject.transform.localScale.x / 4 * 3), (otherObject.transform.localScale.y / 4 * 3), (otherObject.transform.localScale.z / 4 * 3));//Scales object down in the stupidest way imaginable lol
						Debug.Log ("Grabbing object:  " + otherObject.name);
					}

				} else if (holdingObject) {
					holdingObject = false;//So you can pick something up upon next click
					otherObject.GetComponent<Rigidbody> ().useGravity = true;//Turns on gravity on the object your picking up
					otherObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;//Allows object to move freely again
					otherObject.transform.localScale += new Vector3 ((otherObject.transform.localScale.x * 3), (otherObject.transform.localScale.y * 3), (otherObject.transform.localScale.z * 3));//Scales object back up in the stupidest way imaginable lol
					otherObject.transform.parent = null; //Parents The camera to the object
					Debug.Log ("Dropping object:  " + otherObject.name);
					otherObject.transform.SetParent (GameObject.Find ("World").transform, true);//Sets parent back to "World Object"
				}
			} else if (Input.GetButtonDown (placeButton)) {
				if (holdingObject) {
					Ray placeRay = new Ray (transform.position, transform.forward * rayLength);// creates ray for placeing camera
					RaycastHit placeHit;//Creates raycasthit variable for detectign if ray touches wall layermask
					Debug.DrawRay (transform.position, transform.forward * rayLength, Color.red);
					if (Physics.Raycast (placeRay, out placeHit, rayLength, placeLayer)) {
						otherObject.transform.position = new Vector3 (placeHit.point.x, placeHit.point.y, placeHit.point.z);
						otherObject.transform.localScale += new Vector3 ((otherObject.transform.localScale.x * 3), (otherObject.transform.localScale.y * 3), (otherObject.transform.localScale.z * 3));//Scales object back up in the stupidest way imaginable lol
						otherObject.transform.LookAt (transform, Vector3.up);//faces the object towards the player
						otherObject.transform.SetParent (GameObject.Find ("World").transform, true);//Sets parent back to "World Object"
						holdingObject = false;
					}


				} else if (!holdingObject) {
					//add fart noise here (cause it's funny)
				}
			}

		} else if(playerManager.isControllingCam) {
		
		}
	}


	void FixedUpdate ()
	{
		if (holdingObject) {
			otherObject.transform.position = carryPos; //Sets position of object equal to carry marker's position
			otherObject.transform.LookAt(transform,Vector3.up);// the front of the object will always point towards the player

		}
	}
}
