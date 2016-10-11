using UnityEngine;
using System.Collections;

public class AIPathing : MonoBehaviour
{
	/*NOTES:
	 * Set up last know location transform vector and then have the monster go to there after the player breaks sight with it before reseting its
	 * location back to the panic room
	 * 
	 * Name marker in panic room "Panic Room Marker"
	 * Name player one "Player 1" and player two "Player 2"
	 * 
	 */

	private Transform panicRoom;
	private Transform player1;
	private Transform player2;
	public float sightDistance = 25;//How far can the enemy see

	private Transform targetLocation;

	//private Transform p1lastLocation;

	//private bool p1hunting = false;
	//private bool p1Visable = false;


	void Awake(){
		panicRoom = GameObject.Find("Panic Room Marker").GetComponent<Transform>();//Transform of location in Panic Room The enemies must get to to win
		player1 = GameObject.Find("Player1").GetComponent<Transform>();
		player2 = GameObject.Find("Player2").GetComponent<Transform>();
	}

	void Start ()
	{
		
		targetLocation = panicRoom;
	}

	void Update ()
	{
		//Debug.Log (p1hunting);
		//Debug.Log (p1Visable);

		transform.LookAt (targetLocation);
		transform.GetComponent<NavMeshAgent> ().destination = targetLocation.position;

		Ray p1SightRay = new Ray (transform.position, player1.position - transform.position);
		RaycastHit p1Hit;

		Debug.DrawRay (transform.position, player1.position - transform.position);
		if (Physics.Raycast (p1SightRay, out p1Hit, sightDistance)) {

			if (p1Hit.transform.tag == "Player") {
				targetLocation = player1;
			} else {
				targetLocation = panicRoom;
			}


			/*
			if (!p1hunting) {
				
				if (!p1Visable) {
					if (p1Hit.transform.tag == "Player") {
						
						//p1Visable = true;
						targetLocation = player1;
						p1lastLocation.position = player1.position;
					} else {
						
						targetLocation = panicRoom;
					}

				} else if (p1Visable) {
					if (p1Hit.transform.tag == "Player") {
						
						targetLocation = player1;
						p1lastLocation.position = player1.position;

					} else {
						
						p1hunting = true;
						p1Visable = false;

					}
				}
			} else if (p1hunting) {
				if (!p1Visable) {
					if (p1Hit.transform.tag == "Player") {
						
						p1Visable = true;
						p1hunting = false;

					} else {

						targetLocation = p1lastLocation;

					}
				}
			}
			*/
		}
		Ray p2SightRay = new Ray (transform.position, player2.position - transform.position);
		RaycastHit p2Hit;

		if (targetLocation != player1) {
			Debug.DrawRay (transform.position, player2.position - transform.position);
			if (Physics.Raycast (p2SightRay, out p2Hit, sightDistance)) {


				if (targetLocation != player1) {
					targetLocation = player2;
				
				} else {
					targetLocation = panicRoom;
				}
			}
		}
	}


	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player") {
			Destroy (col.gameObject);
			Debug.Log ("touch");
		}
	}
}
