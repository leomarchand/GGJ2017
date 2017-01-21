using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typer : MonoBehaviour {

	public float freq;
	public Text freqText;
	public bool isGoingUp;

	public static float tapInterval;
	public List<float> taps;


	//private float timeStart;
	private static float time; // this one strictly increases
	private float timeWave; // this one gets phase shifted and % 2PI 
	private float bpm;


	// Use this for initialization
	void Start () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		freq = 1f;
		isGoingUp = true;

		tapInterval = 6f;
		taps = new List<float>();

		time = 0f;
		timeWave = 0f;
		bpm = 0f;

		//taps_list.Add(2f);


		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timeWave += Time.deltaTime;

		if (Input.GetButtonDown("Jump")) {
			//bpm += 1f;
			taps.Add(time);
		}

		// calc bpm
		if (taps.Count > 1) {
			taps.RemoveAll(IsTooLongAgo);

			float range = time - taps[0];
			if (range != 0) {
				bpm = taps.Count/range;
			} 
		}

		//if (Input.GetButtonDown("Jump")) {
		// only update every 10ms
		if (Mathf.FloorToInt(time*1000) % 10 == 0) { 
			float oldWave = Mathf.Sin(freq*timeWave);
				//Debug.Log("old timeWave + wave: "+ timeWave + "   " + oldWave);
				// freq += 0.1f;

				// if (isGoingUp) {
				// 	timeWave = Mathf.Asin(oldWave)/freq;
				// } else {
				// 	timeWave = (Mathf.Asin(-oldWave) + Mathf.PI)/freq;				
				// }
			if (bpm != 0) 
				freq = bpm;

			if (isGoingUp) {
				timeWave = Mathf.Asin(oldWave)/freq;
			} else {
				timeWave = (Mathf.Asin(-oldWave) + Mathf.PI)/freq;				
			}
		}

			//Debug.Log("new timeWave + wave: "+ timeWave + "   " + Mathf.Sin(freq*timeWave));
		//}

		float wave = Mathf.Sin(freq*timeWave);
		transform.position = new Vector3(0, wave, 0);
		// keep track of going up or down
		isGoingUp = (Mathf.Sign(Mathf.Cos(freq*timeWave)) > 0f);

		freqText.text = "time: " + time.ToString("0.##") + 
			"\nbpm: " + bpm.ToString("0.##") +
			"\n\ntimeWave: " + timeWave.ToString("0.##") + 
			"\nposition: " + wave.ToString("0.##") + 
			"\nfrequency: " + freq.ToString("0.##");

		// Debug.Log(freqText.text);
		// Debug.Log(Mathf.Sin(timeWave));
		
	}

	// predicate returns true if tapTime occured before the interval
    private static bool IsTooLongAgo(float tapTime)
    {
        return tapTime < time - tapInterval;
    }
}
