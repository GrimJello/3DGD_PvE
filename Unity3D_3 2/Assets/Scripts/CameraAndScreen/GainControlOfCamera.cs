using UnityEngine;
using System.Collections;

public class GainControlOfCamera : MonoBehaviour {

	public GameObject turret;
	private CameraController camControl;
	private PlayerManager playMan;
	private Camera camera;
	private bool isActive;


	// Use this for initialization
	void Start () {
		isActive = false;
		camControl = turret.GetComponent<CameraController> ();
		camera = turret.GetComponentInChildren<Camera> ();
	}
	
	void Update() {
		if (isActive) {
			if ((Input.GetButtonDown ("BackP1") && playMan.player == Player.player1) || (Input.GetButtonDown ("BackP2") && playMan.player == Player.player2)) {
				LoseControl ();
			}
		}
	}

	public void GainControl(PlayerManager playMan) {
		if (!isActive) {
			isActive = true;
			this.playMan = playMan;
			
			camControl.SetControllingPlayerManager(playMan);


			if (playMan.player == Player.player1) {
				camera.rect = new Rect (0, 0, 0.5f, 1);
			} else if (playMan.player == Player.player2) {
				camera.rect = new Rect (0.5f, 0, 0.5f, 1);
			}

			playMan.ControlCamera ();
			camera.enabled = true;

		}
	}

	public void LoseControl() {
		playMan.ControlPlayer ();
		camControl.LoseControl ();
		this.playMan = null;
		camera.enabled = false;
		isActive = false;
	}
}
