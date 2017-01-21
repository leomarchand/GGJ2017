using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typer : MonoBehaviour {

	public float freq;
	public Text freqText;

	//private float timeStart;
	private float time;
	public bool isGoingUp = true;

	// Use this for initialization
	void Start () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		time = 0f;
		freq = 1f;
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (Input.GetButtonDown("Jump")) {
			float oldWave = Mathf.Sin(freq*time);
			Debug.Log("old time + wave: "+ time + "   " + oldWave);
			freq += 0.1f;

			if (isGoingUp) {
				time = Mathf.Asin(oldWave)/freq;
			} else {
				time = (Mathf.Asin(-oldWave) + Mathf.PI)/freq;				
			}
			
			Debug.Log("new time + wave: "+ time + "   " + Mathf.Sin(freq*time));
		}

		float wave = Mathf.Sin(freq*time);
		transform.position = new Vector3(0, wave, 0);
		// keep track of going up or down
		isGoingUp = (Mathf.Sign(Mathf.Cos(freq*time)) > 0f);

		freqText.text = "time: " + time.ToString("0.##") + 
			"\nposition: " + wave.ToString("0.##") + 
			"\nfrequency: " + freq.ToString("0.##");

		// Debug.Log(freqText.text);
		// Debug.Log(Mathf.Sin(time));

		
	}
}
