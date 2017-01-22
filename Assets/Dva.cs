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
		x = 8.0f;//transform.position.x;
		y = -3.5f;//transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (startSpawn && !isSpawning) {
			isSpawning = true;
			transform.position = new Vector3(x, y, 0.0f);
		}




		theta += 0.1f;
		
	}


}
