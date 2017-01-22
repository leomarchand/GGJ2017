using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	public static int levelNum;

	public float freq;
	public Text debugText;
	public Text counterText;
	public Text hintText;
	public Text timeoutText;

	public static float tapInterval;
	public List<float> taps;

	public Sprite happySurfer;
	public Sprite sadSurfer;

	public float happyThreshold; // difference in y that is permissible
	public float happyDuration;

	
	//private float timeStart;

	public static bool isHappy;
	private bool isGoingUp;

	private static float time; // this one strictly increases
	private float timeWave; // this one gets phase shifted and % 2PI 
	private float bpm;
	private GameObject targetWave;
	private Wave targetWaveScript;
	private float happyTimeStart; // every time we get good, start counting down

	private float dvaSpawnTime;
	private bool hasSpawnedDva;

	private float timeoutTime;
	private float timeRemaining;
	

	// Use this for initialization
	void Start () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		freq = 1f;

		tapInterval = 6f;
		taps = new List<float>();

		happyThreshold = MainMenu.happyThreshold;
		happyDuration = MainMenu.happyDuration;

		isGoingUp = true;
		isHappy = false;

		time = 0f;
		timeWave = 0f;
		bpm = 0f;

		targetWave = GameObject.FindWithTag("wave");
		targetWaveScript = targetWave.GetComponent<Wave>();

		dvaSpawnTime = Random.Range(20.0f, 40.0f);
		hasSpawnedDva = false;
		timeoutTime = dvaSpawnTime * 1.5f;

		hintText.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timeWave += Time.deltaTime;

		// if (Input.GetButtonDown("Jump")) {
		// 	Dva.startSpawn = true;
		// }


		if (Input.anyKeyDown) {
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
			timeWave = UpdatePlayerFreq(timeWave);
		}

			//Debug.Log("new timeWave + wave: "+ timeWave + "   " + Mathf.Sin(freq*timeWave));
		//}

		float y = Mathf.Sin(freq*timeWave);
		transform.position = new Vector3(0, y, 0);
		// keep track of going up or down
		isGoingUp = (Mathf.Sign(Mathf.Cos(freq*timeWave)) > 0f);

		// CHECK against the target wave.
		float targetY = targetWaveScript.getYForX(transform.position.x);
		float targetDist = Mathf.Abs(targetY - y);

		if (targetDist < happyThreshold) {
			// happy
			this.GetComponent<SpriteRenderer>().sprite = happySurfer;
			if (!isHappy) {
				happyTimeStart = time;
				isHappy = true;
			}

		} else {
			this.GetComponent<SpriteRenderer>().sprite = sadSurfer;
			isHappy = false;
		}


		// secret DVA display condition
		if (levelNum > 6 && time > dvaSpawnTime && !hasSpawnedDva) {
			// spawn dva
			Dva.startSpawn = true;
			hasSpawnedDva = true;
		}

		// display the timeout text
		timeRemaining = timeoutTime - time;

		if (timeRemaining < 10.0f) {
			timeoutText.text = timeRemaining.ToString("0") + "s until giving up" +
				"\n" + timeRemaining.ToString("0") + "초 후에 포기";
		} else {
			timeoutText.text = "";
		}

		// display the instruction text on level 1
		if (levelNum < 3) {
			if (time < 10f) {
				if (Mathf.FloorToInt(time*5) % 2 == 1) {
					hintText.enabled = true;
				} else {
					hintText.enabled = false;
				}
			} else {
				hintText.enabled = false;
			}
		}

		// display the countdown
		if (isHappy) {
			float happyTimeElapsed = time - happyTimeStart;
			float countStep = happyDuration / 3f; //divide the duration by 3

			if (happyTimeElapsed < countStep) {
				counterText.text = "3";
			} else if (happyTimeElapsed < 2f * countStep) {
				counterText.text = "2";
			} else if (happyTimeElapsed < happyDuration) {
				counterText.text = "1";
			} else {
				counterText.text = "0";
				// WIN!

				// increase difficulty
				levelNum++;
				MainMenu.happyThreshold -= 0.04f;

				Wave.minFreq += 0.02f;
				Wave.maxFreq += 0.1f;

				Wave.multiplier = Random.Range(Wave.minFreq, Wave.maxFreq);

				MainMenu.happyDuration = 0.22f / Wave.multiplier + 0.52f;

				SceneManager.LoadScene ("greetings");
			}




		} else {
			counterText.text = "";
		}


		debugText.text = "level: " + levelNum.ToString("0.##") +  
			"\n happyThreshold: " + happyThreshold.ToString("0.##") +
			"\n happyDuration: " + happyDuration.ToString("0.##") +
			"\n difficulty: " + Wave.multiplier.ToString("0.##") + 
			//"\ntime: " + time.ToString("0.##") + 
			//"\nbpm: " + bpm.ToString("0.##") +
			//"\n\ntimeWave: " + timeWave.ToString("0.##") + 
			//"\nposition.y: " + y.ToString("0.##") + 
			"\nfrequency: " + freq.ToString("0.##");
			//"\n\ntargetY: " + targetY.ToString("0.##") +
			//"\n targetDist: " + targetDist.ToString("0.##");

		// Debug.Log(freqText.text);
		// Debug.Log(Mathf.Sin(timeWave));
		
	}

	private float UpdatePlayerFreq(float t) {
		float oldWave = Mathf.Sin(freq*t);
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
			t = Mathf.Asin(oldWave)/freq;
		} else {
			t = (Mathf.Asin(-oldWave) + Mathf.PI)/freq;				
		}
		return t;
	}

	// predicate returns true if tapTime occured before the interval
    private static bool IsTooLongAgo(float tapTime)
    {
        return tapTime < time - tapInterval;
    }
}
