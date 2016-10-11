// Based on code from Unity in Action

using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseX = 0,
        MouseY = 1
    }
    public RotationAxes axes = RotationAxes.MouseX;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;

	private string lookAxisX;
	private string lookAxisY;

	private PlayerManager playerManager;

    void Start()
    {

		playerManager = GetComponentInParent<PlayerManager>();

		//setting controls
		if (playerManager.player == Player.player1) {
			lookAxisX = "HorizontalLookP1";
			lookAxisY = "VerticalLookP1";
		} else if (playerManager.player == Player.player2) {
			lookAxisX = "HorizontalLookP2";
			lookAxisY = "VerticalLookP2";
		}
			
        // Hide the cursor and snap it's position to the middle of the window
        Cursor.lockState = CursorLockMode.Locked;
        
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    void Update()
	{	
		if (!playerManager.isControllingCam) {
			if (axes == RotationAxes.MouseX) {
				transform.Rotate (0, Input.GetAxis (lookAxisX) * sensitivityHor, 0);
			} else if (axes == RotationAxes.MouseY) {
				_rotationX += Input.GetAxis (lookAxisY) * sensitivityVert;
				_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert);

				transform.localEulerAngles = new Vector3 (_rotationX, transform.localEulerAngles.y, 0);
			}
		
		} else if (playerManager.isControllingCam) {
			
		}
	}
}
