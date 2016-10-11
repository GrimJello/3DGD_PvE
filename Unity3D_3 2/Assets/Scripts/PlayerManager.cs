using UnityEngine;
using System.Collections;


public enum Player {
	player1 = 1,
	player2 = 2,
	noPlayer = 3,
}
public class PlayerManager : MonoBehaviour {

	public bool isControllingCam;
	public Player player;

	public void ControlCamera() {
		isControllingCam = true;
	}

	public void ControlPlayer() {
		isControllingCam = false;
	}


}
