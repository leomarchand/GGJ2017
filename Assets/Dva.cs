using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dva : MonoBehaviour {

	public static bool startSpawn;

	private bool isSpawning;
	private float theta;

	private float x;
	private float y;

	// Use this for initialization
	void Start () {
		startSpawn = false;
		theta = 0f;
		x = transform.position.x;
		y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (startSpawn && !isSpawning) {
			isSpawning = true;
			transform.position = new Vector3(8, -3.5, 0);
		}




		theta += 0.1f;
		
	}


}
