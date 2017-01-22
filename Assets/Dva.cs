using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dva : MonoBehaviour {

	public static bool startSpawn;

	private bool isSpawning;
	private float theta, thetaIncrement;

	private float x, xInit, y, yInit;

	// Use this for initialization
	void Start () {
		startSpawn = false;
		isSpawning = false; 

		
		thetaIncrement = 0.05f;
		xInit = 12.0f;//transform.position.x;
		yInit = -7.5f;//transform.position.y;

		theta = 0f;
		x = xInit;
		y = yInit;
		transform.position = new Vector3(x, y, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (startSpawn && !isSpawning) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play ();
			isSpawning = true;
			transform.position = new Vector3(x, y, 0.0f);
		}

		if (isSpawning) {
			if (theta > Mathf.PI + thetaIncrement) { // we done yo
				startSpawn = false;
				isSpawning = false;
				theta = 0f;
				x = xInit;
				y = yInit;
			} else if (theta > Mathf.PI/2f - thetaIncrement && theta < Mathf.PI/2f + thetaIncrement) {
				// in the middle, hover for a bit
				updateXY();
				theta += thetaIncrement/35.0f;
			} else {
				updateXY();
				theta += thetaIncrement;
			}
		}
	}

	private void updateXY() {
		x = xInit - 4f*Mathf.Sin(theta);
		y = yInit + 4f*Mathf.Sin(theta);
		transform.position = new Vector3(x, y, 0.0f);
	} 


}
