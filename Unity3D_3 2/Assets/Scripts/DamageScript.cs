using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	public int health;

	private AudioSource audio;

	public AudioClip takeDamage;
	public AudioClip die;

	void Start() {
		audio = GetComponent<AudioSource> ();
	}

	public void TakeDamage(int damage) {
		health -= damage;


		if (health > 0) {
			audio.pitch = Random.Range(0.8f, 1.2f);
			audio.PlayOneShot (takeDamage);
		} else if (health <= 0) {
			StartCoroutine ("DestroyThisObject");
		}

	}

	IEnumerator DestroyThisObject() {
		audio.pitch = Random.Range(0.8f, 1.2f);
		audio.PlayOneShot (die);

		while (true) {
			transform.localScale -= new Vector3 (0.2f, 0.2f, 0.2f);
			yield return new WaitForSeconds (0.1f);

			if (transform.localScale.x <= 0) {
				Destroy (gameObject);
			}
		}


	}
}
