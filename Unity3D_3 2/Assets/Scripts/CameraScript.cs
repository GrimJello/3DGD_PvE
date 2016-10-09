using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private float batteryCharge;
	public float startCharge;

	public float rechargeRate;

	public float drainRate;

	private CamManager camManager;

	//check if the camera is being carried by p1
	public bool beingCarried;

	//check if the camera is currently being used by p2
	public bool isOccupied;

	//check if this is the default camera where p2 starts with at the beginning 
	public bool isStartCam;

	private Camera thisCam;

	private float viewingAngleH;
	private float viewingAngleV;

	private float maxViewingAngleH;
	private float maxViewingAngleV;

	void Start() {
		camManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<CamManager> ();

		batteryCharge = startCharge;
		thisCam = GetComponentInChildren<Camera> ();
	}


	public void SetOccupied(bool isOccupied) {
		this.isOccupied = isOccupied;
		thisCam.enabled = isOccupied;

		if (isOccupied) {
			StartCoroutine ("DrainBattery");
			StopCoroutine ("RechargeBattery");

		} else {
			StopCoroutine ("DrainBattery");
			StartCoroutine ("RechargeBattery");
		}
	}

	IEnumerator DrainBattery() {
		while (true) {
			Color color = Color.Lerp (Color.red, Color.white, batteryCharge / startCharge);
			float scale = (batteryCharge / startCharge);

			camManager.UpdateBatteryUI (scale, color);
			yield return new WaitForSeconds (0.1f);
			batteryCharge -= drainRate;

			if (batteryCharge <= 0) {
				batteryCharge = 0;
				camManager.NextCam ();
				break;
			}
		}
	}

	IEnumerator RechargeBattery() {
		while (batteryCharge < startCharge) {
			batteryCharge++;
			yield return new WaitForSeconds (rechargeRate);
				
		}

		batteryCharge = startCharge;
	}

}
