using UnityEngine;
using System.Collections;

public class CamShotManager : MonoBehaviour {

	//the light that is currently being manipulated
	private Light light;

	//the increment in which the light becomes more intense
	public float lightStep;

	//the intensity max and min when shooting and not shooting
	public float maxIntensity;
	public float minIntensity;

	//the increment in which the angle of the lightcone becomes bigger
	public float angleStep;

	//the angle max and min when shooting and not shooting
	public float maxAngle;
	public float minAngle;

	//boolean that logs if the camera is shooting
	private bool isShooting;

	void Start () {
		//getting the light
		light = GetComponent<Light> ();
	}


	//deal damage to all enemies in trigger
	void OnTriggerStay(Collider other) {
		if (isShooting) {
			if (other.gameObject.CompareTag("enemy")) {
				Destroy (other.gameObject, Random.Range(0.5f, 1.0f)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   );
			}
		}
	}

	//the coroutine that starts shooting, sets the bool and moves cone intensity and angle to the shooting state
	public IEnumerator Shooting() {
		isShooting = true;


		while (true) {
			if (light.intensity < maxIntensity) {
				light.intensity += lightStep;
			} else {
				light.intensity = maxIntensity;
			}

			if (light.spotAngle < maxAngle) {
				light.spotAngle += angleStep;
			} else {
				light.spotAngle = maxAngle;

			}

			yield return new WaitForEndOfFrame ();
		}


	}

	//the coroutine that stops shooting, sets the bool and moves cone intensity and angle to the non-shooting state
	public IEnumerator StopShooting() {
		isShooting = false;

		while (true) {

			if (light.intensity > minIntensity) {
				light.intensity -= lightStep;
			} else {
				light.intensity = minIntensity;
			}

			if (light.spotAngle > minAngle) {
				light.spotAngle -= angleStep;
			} else {
				light.spotAngle = minAngle;
			}

			if (light.spotAngle == minAngle && light.intensity == minIntensity) {
				break;
			}

			yield return new WaitForEndOfFrame ();
		}

		//make sure that everything is set correctly
		light.intensity = minIntensity;
		light.spotAngle = minAngle;
	}
}
