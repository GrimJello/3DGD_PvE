using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private PlayerManager controllingPlayerManager;
	private Player player;

	private CamLightShootingManager camLightShootingManager;


	void Start() {
		player = Player.noPlayer;
		camLightShootingManager = GetComponentInChildren<CamLightShootingManager> ();
	}

	//this function will be called from the display that is tied to this camera
	public void SetControllingPlayerManager(PlayerManager playerManager) {
		controllingPlayerManager = playerManager;
		player = playerManager.player;
	}


	
	// Update is called once per frame
	void Update () {
		
		if (player == Player.player1) {
			float deltaX =Input.GetAxis ("HorizontalP1");

			float deltaY = Input.GetAxis ("VerticalP1");

			//camMan.RotateCurrentCam (deltaX * sensitivityV, deltaY * sensitivityH);

			if (Input.GetAxis ("FireP1") > 0.3f) {
				Debug.Log ("i am under control");

				camLightShootingManager.StartCoroutine ("Shooting");
				camLightShootingManager.StopCoroutine ("StopShooting");
			} else if (Input.GetAxis ("FireP1") <= 0.3f) {
				camLightShootingManager.StopCoroutine ("Shooting");
				camLightShootingManager.StartCoroutine ("StopShooting");
			}
		

		} else if (player == Player.player2) {

			float deltaX =Input.GetAxis ("HorizontalP2");

			float deltaY = Input.GetAxis ("VerticalP2");

			if (Input.GetAxis ("FireP2") > 0.3f) {
				camLightShootingManager.StartCoroutine ("Shooting");
				camLightShootingManager.StopCoroutine ("StopShooting");
			} else if (Input.GetAxis ("FireP2") <= 0.3f) {
				camLightShootingManager.StopCoroutine ("Shooting");
				camLightShootingManager.StartCoroutine ("StopShooting");
			}
		}
	}


	public void LoseControl() {
		player = Player.noPlayer;
		camLightShootingManager.StopCoroutine ("Shooting");
		camLightShootingManager.StartCoroutine ("StopShooting");

	}



}
