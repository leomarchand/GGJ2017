using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAsol : MonoBehaviour {

	private GameObject targetWave;
	private Wave targetWaveScript;
	private float xPosition;

	// Use this for initialization
	void Start () {
		xPosition = 8.75f;
		targetWave = GameObject.FindWithTag("wave");
		targetWaveScript = targetWave.GetComponent<Wave>();
	}
	
	// Update is called once per frame
	void Update () {
		var sprite = this.GetComponent<SpriteRenderer>().sprite;
		Vector3 position = new Vector3(xPosition, targetWaveScript.getYForX(xPosition));
		transform.position = position;
	}
}
