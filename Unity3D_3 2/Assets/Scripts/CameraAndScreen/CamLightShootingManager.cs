using UnityEngine;
using System.Collections;

public class CamLightShootingManager : MonoBehaviour {

	//the light that is currently being manipulated
	private Light light;

	//the increment in which the light becomes more intense
	public float lightStep;

	//the intensity max and min when shooting and not shooting
	public float shootingIntensity;
	public float nonShootingIntensity;

	//the increment in which the angle of the lightcone becomes bigger
	public float angleStep;

	//the angle max and min when shooting and not shooting
	public float shootingAngle;
	public float nonShootingAngle;

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
			if (light.intensity > shootingIntensity) {
				light.intensity -= lightStep;
			} else {
				light.intensity = shootingIntensity;
			}

			if (light.spotAngle > shootingAngle) {
				light.spotAngle -= angleStep;
			} else {
				light.spotAngle = shootingAngle;

			}

			yield return new WaitForEndOfFrame ();
		}


	}

	//the coroutine that stops shooting, sets the bool and moves cone intensity and angle to the non-shooting state
	public IEnumerator StopShooting() {
		isShooting = false;

		while (true) {

			if (light.intensity < nonShootingIntensity) {
				light.intensity += lightStep;
			} else {
				light.intensity = nonShootingIntensity;
			}

			if (light.spotAngle < nonShootingAngle) {
				light.spotAngle += angleStep;
			} else {
				light.spotAngle = nonShootingAngle;
			}

			if (light.spotAngle == nonShootingAngle && light.intensity == nonShootingIntensity) {
				break;
			}

			yield return new WaitForEndOfFrame ();
		}

		//make sure that everything is set correctly
		light.intensity = nonShootingIntensity;
		light.spotAngle = nonShootingAngle;
	}
}
