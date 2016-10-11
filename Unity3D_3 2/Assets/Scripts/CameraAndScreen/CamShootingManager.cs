using UnityEngine;
using System.Collections;

public class CamShootingManager : MonoBehaviour {

	//the light that is currently being manipulated
	private Light light;

	//the light for the muzzleFlash
	private Light muzzleLight; 

	//bang for shooting
	private AudioSource audioSource;

	public AudioClip shootingClip;

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
		muzzleLight = transform.FindChild ("Barrel").GetComponent<Light> ();
		muzzleLight.enabled = false;

		audioSource = transform.FindChild ("Barrel").GetComponent<AudioSource> ();

	}
		

	//the coroutine that starts shooting, sets the bool and moves cone intensity and angle to the shooting state
	public IEnumerator Shooting() {

		if (!isShooting) {
			StartCoroutine ("DealDamage");
		}

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

		if (isShooting) {
			StopCoroutine ("DealDamage");
		}

		isShooting = false;
		muzzleLight.enabled = false;

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


	IEnumerator DealDamage() {
		yield return new WaitForSeconds (0.8f);

		while (true) {
			yield return new WaitForSeconds (Random.Range(0.3f, 0.4f));
			muzzleLight.enabled = true;
			audioSource.pitch = Random.Range (0.8f, 1.2f);
			audioSource.PlayOneShot (shootingClip);

			RaycastHit hit;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast (transform.position, fwd, out hit, 50f)) {

				GameObject hitObject = hit.collider.gameObject;

				if(hitObject.name == "MurderMan(Clone)") {
					hitObject.GetComponent<DamageScript> ().TakeDamage (5);
				}
			}

			yield return new WaitForSeconds (0.1f);
			muzzleLight.enabled = false;

		}
	}
}
