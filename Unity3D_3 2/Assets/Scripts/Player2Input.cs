using UnityEngine;
using System.Collections;

public class Player2Input : MonoBehaviour {

	public CamManager camMan;

	public float sensitivityH;
	public float sensitivityV;

	// Use this for initialization
	void Start () {
		camMan = GetComponent<CamManager> ();
	}
	
	// Check for different inputs on every frame
	void Update () {

		CheckForCameraSwap ();

		CheckForAngleChange ();

		CheckForShooting ();

	}

	//duh
	void CheckForCameraSwap() {
		if (Input.GetButtonDown ("ChangeCamLeft")) {
			camMan.PreviousCam ();
		} else if (Input.GetButtonDown ("ChangeCamRight")) {
			camMan.NextCam ();
		}
	}

	//check if camera is being rotated
	void CheckForAngleChange() {

		float deltaX =Input.GetAxis ("HorizontalP2");

		float deltaY = Input.GetAxis ("VerticalP2");

		camMan.RotateCurrentCam (deltaX * sensitivityV, deltaY * sensitivityH);

	}

	//checks if there three little pigs visible in the camera field of view and spawns a big bad wolf to blow down their house
	void CheckForShooting() {

		//0.3 is just some value that felt good, i am gonna leave it here hardcoded to not clutter up the code
		if (Input.GetAxis ("FireCam") > 0.3f) {
			camMan.CamShooting (true);
		} else if (Input.GetAxis ("FireCam") <= 0.3f) {
			camMan.CamShooting (false);

		}
	}
}
