using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	private PlayerManager controllingPlayerManager;
	private Player player;

	private CamShootingManager camShootingManager;

	private Vector3 surfaceNormal;
	private Quaternion lastKnownAngle;

	public float sensitivityH;
	public float sensitivityV;

	public Text ammoText; 

	void Start() {
		player = Player.noPlayer;
		camShootingManager = GetComponent<CamShootingManager> ();

	}



	
	// Update is called once per frame
	void Update () {
		
		if (player == Player.player1) {
			if (Mathf.Abs(Input.GetAxis ("HorizontalLookP1")) > 0.4f) {

					float deltaX = Input.GetAxis ("HorizontalLookP1");
					transform.Rotate (new Vector3 (0, deltaX * sensitivityV, 0), Space.World);

			}
		

			if (Mathf.Abs(Input.GetAxis ("VerticalLookP1")) > 0.4f) {
					
					float deltaY = Input.GetAxis ("VerticalLookP1");
					transform.Rotate (new Vector3 (deltaY * sensitivityH, 0, 0), Space.Self);
			}

		
			

			if (Input.GetAxis ("FireP1") > 0.3f && camShootingManager.ammunition > 0) {

					camShootingManager.StartCoroutine ("Shooting");
					camShootingManager.StopCoroutine ("StopShooting");
					ammoText.text = camShootingManager.ammunition.ToString() + "/" + camShootingManager.startingAmmo.ToString();
					Debug.Log ("Ammotext: " + ammoText + " Shootman: " + camShootingManager);

			} else {
					camShootingManager.StopCoroutine ("Shooting");
					camShootingManager.StartCoroutine ("StopShooting");
					ammoText.text = camShootingManager.ammunition.ToString() + "/" + camShootingManager.startingAmmo.ToString();
			}
		

		} else if (player == Player.player2) {
			if (Mathf.Abs(Input.GetAxis ("HorizontalLookP2")) > 0.4f) {
					float deltaX = Input.GetAxis ("HorizontalLookP2");
					transform.Rotate (new Vector3 (0, deltaX * sensitivityV, 0), Space.World);

			}

			if (Mathf.Abs(Input.GetAxis ("VerticalLookP2")) > 0.4f) {
					
					float deltaY = Input.GetAxis ("VerticalLookP2");
					transform.Rotate (new Vector3 (deltaY * sensitivityH, 0, 0), Space.Self);

			}


			if ((Input.GetAxis ("FireP2") > 0.3f) && camShootingManager.ammunition > 0) {


				camShootingManager.StartCoroutine ("Shooting");
				camShootingManager.StopCoroutine ("StopShooting");
				ammoText.text = camShootingManager.ammunition.ToString() + "/" + camShootingManager.startingAmmo.ToString();

			} else {

				camShootingManager.StopCoroutine ("Shooting");
				camShootingManager.StartCoroutine ("StopShooting");
				ammoText.text = camShootingManager.ammunition.ToString() + "/" + camShootingManager.startingAmmo.ToString();

			}
		}

		if (surfaceNormal != null && player!= null) {
			if (Vector3.Angle (transform.forward, surfaceNormal) > 60f) {
				transform.rotation = lastKnownAngle;
			} else {
				lastKnownAngle = transform.rotation;
			}
		}

	
	}

	//this function will be called from the display that is tied to this camera
	public void SetControllingPlayerManager(PlayerManager playerManager, Text ammo) {
		controllingPlayerManager = playerManager;
		player = playerManager.player;
		ammoText = ammo;
	}


	public void LoseControl() {
		player = Player.noPlayer;
		camShootingManager.StopCoroutine ("Shooting");
		camShootingManager.StartCoroutine ("StopShooting");
		ammoText = null;
	}

	public void SetSurfaceNormal(Vector3 normal) {
		surfaceNormal = normal;
		transform.forward = normal;
	}

}
