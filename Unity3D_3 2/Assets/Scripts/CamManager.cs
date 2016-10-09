using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//class that manages all the cameras that player 2 can use
public class CamManager : MonoBehaviour {


	//all the cameras in the scene that player can use
	public List<CameraScript> availableCams = new List<CameraScript>();

	//set this bool true on the starting Camera
	private bool startCamSet = false;

	private Vector3 startScale;

	//index of the current cam
	private int currentCamIndex;

	//light of the current camera
	private Light currentLight;

	public Image batteryChargeImage;

	public float drainRate;
	public float drainRateFiring;



	void Start() {
		startScale = batteryChargeImage.rectTransform.localScale;

		//create list of all cameras with tag p2cam
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("p2cam")) {

			CameraScript cam = obj.GetComponentInChildren<CameraScript> ();

			if (cam != null) {
				availableCams.Add (cam);

			} else {
				
				Debug.Log ("No cam found on object with tag");

			}

		}

		//set active p2 camera to first camera with boolean isStartCam true
		for (int i = 0; i < availableCams.Count; i++) {
			if (availableCams [i].isStartCam == true && startCamSet == false) {
				SetActiveCamera (availableCams [i]);
				startCamSet = true;
			} else {
				availableCams [i].SetOccupied(false);
			}
		}

	}

	//this is the actual function that sets the active camera, it receives a camerascript and sets all the "current___" variables to the components of that camera
	void SetActiveCamera(CameraScript cam) {

		for (int i = 0; i < availableCams.Count; i++) {
			
			if (cam == availableCams [i]) {
				
				cam.SetOccupied (true);
				currentCamIndex = i;

			} else {
				//set the "occupied" of all other cams to false, so they cant be controlled anymore
				availableCams [i].SetOccupied (false);
			}
		}
	}

	//changes to the next cam
	public void NextCam() {
		//kinda bad style to put this here, but the formerly selected cam will have to stop "shooting"
		CamShooting (false);

		currentCamIndex = (currentCamIndex + 1) % availableCams.Count;
		SetActiveCamera (availableCams [currentCamIndex]);
	}


	//changes to the previous cam
	public void PreviousCam() {
		//kinda bad style to put this here, but the formerly selected cam will have to stop "shooting"
		CamShooting (false);

		if (currentCamIndex > 0) {
			currentCamIndex = currentCamIndex - 1;
		} else {
			currentCamIndex = availableCams.Count - 1;
		}
		SetActiveCamera (availableCams [currentCamIndex]);
	}

	//sets the transform of the currently selected cam to rotate it
	public void RotateCurrentCam(float deltaX, float deltaY) {
		availableCams [currentCamIndex].transform.Rotate(new Vector3(0, deltaX, 0), Space.World);
		availableCams [currentCamIndex].transform.Rotate(new Vector3(deltaY, 0, 0), Space.Self);
	}


	//stops and starts shooting the current cam
	public void CamShooting(bool isShooting) {
		CamShotManager shootingLight = availableCams [currentCamIndex].gameObject.GetComponentInChildren<CamShotManager> ();

		if (isShooting) {
			shootingLight.StartCoroutine ("Shooting");
			shootingLight.StopCoroutine ("StopShooting");
			availableCams [currentCamIndex].drainRate = drainRateFiring;

		} else {
			shootingLight.StartCoroutine("StopShooting");
			shootingLight.StopCoroutine ("Shooting");
			availableCams [currentCamIndex].drainRate = drainRate;

		}
	}

	public void UpdateBatteryUI(float charge, Color color) {
		batteryChargeImage.rectTransform.localScale = new Vector3( startScale.x * charge, startScale.y , startScale.z );
		batteryChargeImage.color = color;
	}

}
