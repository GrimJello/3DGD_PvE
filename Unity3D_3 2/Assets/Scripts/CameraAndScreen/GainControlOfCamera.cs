using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GainControlOfCamera : MonoBehaviour {

	public GameObject turret;
	private CameraController camControl;
	private PlayerManager playMan;
	private Camera camera;
	private bool isActive;
	public RenderTexture rendTex;

	public Text ammoTextP1;
	public Text ammoTextP2;



	// Use this for initialization
	void Start () {

		if (rendTex == null) {
			Debug.Log ("Please assign rendertexture in script");
		}

		isActive = false;
		camControl = turret.GetComponentInChildren<CameraController> ();
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
			camera.targetTexture = null;
			


			if (playMan.player == Player.player1) {

				camera.rect = new Rect (0, 0, 0.5f, 1);
				Debug.Log ("asdf");

				ammoTextP1.enabled = true;
				camControl.SetControllingPlayerManager(playMan, ammoTextP1);

			} else if (playMan.player == Player.player2) {
				camera.rect = new Rect (0.5f, 0, 0.5f, 1);

				ammoTextP2.enabled = true;
				camControl.SetControllingPlayerManager(playMan, ammoTextP2);

			}

			playMan.ControlCamera ();
			camera.enabled = true;

		}
	}

	public void LoseControl() {

		if (playMan.player == Player.player1) {
			ammoTextP1.enabled = false;
		} else if (playMan.player == Player.player2) {
			ammoTextP2.enabled = false;
		}

		camera.targetTexture = rendTex;
		playMan.ControlPlayer ();
		camControl.LoseControl ();
		this.playMan = null;
		camera.enabled = true;
		isActive = false;
	}
}
