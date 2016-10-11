using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private PlayerManager controllingPlayerManager;
	private Player player;

	private CamShootingManager camShootingManager;

	public float sensitivityH;
	public float sensitivityV;

	void Start() {
		player = Player.noPlayer;
		camShootingManager = GetComponentInChildren<CamShootingManager> ();
	}



	
	// Update is called once per frame
	void Update () {
		
		if (player == Player.player1) {
			if (Mathf.Abs(Input.GetAxis ("HorizontalP1")) > 0.4f) {
				float deltaX = Input.GetAxis ("HorizontalP1");
				transform.Rotate (new Vector3 (0, deltaX * sensitivityV, 0), Space.World);
			}

			if (Mathf.Abs(Input.GetAxis ("VerticalP1")) > 0.4f) {
				float deltaY = Input.GetAxis ("VerticalP1");
				transform.Rotate(new Vector3(deltaY * sensitivityH, 0, 0), Space.Self);

			}
			

			if (Input.GetAxis ("FireP1") > 0.3f) {

				camShootingManager.StartCoroutine ("Shooting");
				camShootingManager.StopCoroutine ("StopShooting");

			} else if (Input.GetAxis ("FireP1") <= 0.3f) {
				camShootingManager.StopCoroutine ("Shooting");
				camShootingManager.StartCoroutine ("StopShooting");

			}
		

		} else if (player == Player.player2) {
			if (Mathf.Abs(Input.GetAxis ("HorizontalP2")) > 0.4f) {
				float deltaX = Input.GetAxis ("HorizontalP2");
				transform.Rotate (new Vector3 (0, deltaX * sensitivityV, 0), Space.World);
			}

			if (Mathf.Abs(Input.GetAxis ("VerticalP2")) > 0.4f) {
				float deltaY = Input.GetAxis ("VerticalP2");
				transform.Rotate(new Vector3(deltaY * sensitivityH, 0, 0), Space.Self);
			}


			if (Input.GetAxis ("FireP2") > 0.3f) {

				camShootingManager.StartCoroutine ("Shooting");
				camShootingManager.StopCoroutine ("StopShooting");


			} else if (Input.GetAxis ("FireP2") <= 0.3f) {

				camShootingManager.StopCoroutine ("Shooting");
				camShootingManager.StartCoroutine ("StopShooting");

			}
		}

	}

	//this function will be called from the display that is tied to this camera
	public void SetControllingPlayerManager(PlayerManager playerManager) {
		controllingPlayerManager = playerManager;
		player = playerManager.player;
	}


	public void LoseControl() {
		player = Player.noPlayer;
		camShootingManager.StopCoroutine ("Shooting");
		camShootingManager.StartCoroutine ("StopShooting");
	}



}
