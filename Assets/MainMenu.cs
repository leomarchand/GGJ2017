using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public static float happyThreshold; // difference in y that is permissible
	public static float happyDuration;

	// Use this for initialization
	void Start () {
		Level.levelNum = 1;
		Wave.minFreq = 0.1f;
		Wave.maxFreq = 0.25f;
		Wave.multiplier = Random.Range(Wave.minFreq, Wave.maxFreq);
		
		happyThreshold = 1.0f;
		happyDuration = 0.22f / Wave.multiplier + 0.52f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("greetings");
		}

	}
}
