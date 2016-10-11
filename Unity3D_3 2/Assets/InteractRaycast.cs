using UnityEngine;
using System.Collections;

public class InteractRaycast : MonoBehaviour {

	private PlayerManager playMan;

	void Start() {
		playMan = GetComponentInParent<PlayerManager> ();
	}

	void Update () {
		if (playMan.player == Player.player1 && Input.GetButtonDown ("InteractP1")) {
			Interact ();
		} else if (playMan.player == Player.player2 && Input.GetButtonDown ("InteractP2")) {
			Interact ();
		}
	}

	public void Interact() {
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		if (Physics.Raycast(transform.position, fwd, out hit, 2f)) {
			GameObject hitObject = hit.collider.gameObject;
			if (hitObject.CompareTag("Monitor")) {
				hitObject.GetComponent<GainControlOfCamera> ().GainControl(playMan);
			}
		}
	}
}
