using UnityEngine;
using System.Collections;

public class CamShootingManager : MonoBehaviour {

	public GameObject bulletHole;

	public int ammunition;
	public int startingAmmo;

	//the light that is currently being manipulated
	private Light light;

	//the light for the muzzleFlash
	private Light muzzleLight; 

	//bang for shooting
	private AudioSource audioSource;

	public AudioClip shootingClip;
	public AudioClip reloadClip;

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

	public Color shootingColor;
	private Color startingColor;


	private ParticleSystem muzzleFlash;
	private ParticleSystem bulletParticle;

	//boolean that logs if the camera is shooting
	private bool isShooting;

	void Start () {
		//getting the light
		light = GetComponent<Light> ();

		ammunition = startingAmmo;

		startingColor = light.color;

		muzzleLight = transform.FindChild ("Barrel").GetComponent<Light> ();
		muzzleLight.enabled = false;

		audioSource = transform.FindChild ("Barrel").GetComponent<AudioSource> ();
		muzzleFlash = transform.FindChild("Barrel").FindChild("Muzzleflash").GetComponent<ParticleSystem> ();
		bulletParticle = transform.FindChild ("Barrel").FindChild ("BulletParticle").GetComponent<ParticleSystem> ();
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
				light.color = Color.red;
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
			light.color = Color.white;


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
		if (ammunition > 0) {
			yield return new WaitForSeconds (0.8f);

			while (ammunition > 0) {
				muzzleLight.enabled = true;
				muzzleFlash.Emit (1);
				bulletParticle.Emit (1);
				ammunition--;
				//camInterface.UpdateInterface ();

				yield return new WaitForSeconds (Random.Range (0.02f, 0.03f));

				muzzleLight.enabled = false;
				audioSource.pitch = Random.Range (0.8f, 1.2f);
				audioSource.PlayOneShot (shootingClip);



				RaycastHit hit;
				Vector3 fwd = transform.TransformDirection (Vector3.forward);
				if (Physics.Raycast (transform.position, fwd, out hit, 50f)) {

					GameObject hitObject = hit.collider.gameObject;

					if (hitObject.name == "MurderMan(Clone)") {
						hitObject.GetComponent<DamageScript> ().TakeDamage (5);
					} else if (hitObject.CompareTag ("Wall")) {
						GameObject obj = Instantiate (bulletHole, hit.point, Random.rotation) as GameObject;
						obj.transform.forward = hit.normal;
						//obj.transform.rotation = Quaternion.Euler(new Vector3 ( Random.Range (0f, 180f),obj.transform.rotation.y,obj.transform.rotation.z ));
						obj.transform.Translate (-hit.normal * 0.01f);
					}
				}
				Recoil ();

				yield return new WaitForSeconds (0.03f);

			}
		}
	}

	private void Recoil() {
		transform.Rotate (new Vector3 (Random.Range(-5f, 1f), 0, 0), Space.Self);
		transform.Rotate (new Vector3 (0, Random.Range(-2f, 2f), 0), Space.World);

	}

	public void ReloadWeapon() {
		ammunition = startingAmmo;
		audioSource.PlayOneShot (reloadClip);
	}

}
