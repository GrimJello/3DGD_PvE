using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExchangeObject : MonoBehaviour {

	public List<GameObject> objects = new List<GameObject>();
	private GameObject currentObject;
	private Renderer rend;

	private bool objectRerolled;

	// Use this for initialization
	void Start () {


		//put all children into list
		foreach (Transform child in gameObject.transform) {
			objects.Add (child.gameObject);
		}

		ReRollObject ();

	}


	void Update() {
		Debug.Log (rend);
		if (!rend.isVisible && !objectRerolled) {
			ReRollObject ();
			objectRerolled = true;
		} 
		if (rend.isVisible) {
			objectRerolled = false;
		}
	}


	void ReRollObject() {
		int rando = Random.Range (0, objects.Count);


		foreach (GameObject obj in objects) {
			obj.SetActive (false);

			if (obj == objects [rando]) {
				obj.SetActive (true);
				currentObject = obj;
				rend = currentObject.GetComponent<Renderer> ();

			}

		}
	}
}
