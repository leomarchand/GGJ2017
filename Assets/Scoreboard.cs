using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

	private struct highscore {
		public string name;
		public int score;

		public highscore(string name, int score) {
            this.name = name;
			this.score = score;
        }
	}

	private List<highscore> scoreList;


	// Use this for initialization
	void Start () {
		// test debug

		scoreList = new List<highscore>();

		scoreList.Add(new highscore("MACHO", 69));
		scoreList.Add(new highscore("MJ KIM", 68));

		Debug.Log(scoreList[0].name);
		Debug.Log(scoreList[1].score);

		for (int i = 0; i < scoreList.Count; i++) {

			PlayerPrefs.SetString(i.ToString() + "name", scoreList[0].name);
			PlayerPrefs.SetInt(i.ToString() + "score", scoreList[0].score);

		}

		PlayerPrefs.Save(); 

		// load scores from playerprefs


		// update scores

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
