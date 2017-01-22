using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	public Text firstText;
	public Text secondText;
	public Text thirdText;
	public Text fourthText;
	public Text fifthText;

	public static string playerName;
	public static int playerScore;

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

			PlayerPrefs.SetString(i.ToString() + "name", scoreList[i].name);
			PlayerPrefs.SetInt(i.ToString() + "score", scoreList[i].score);

		}

		PlayerPrefs.Save(); 

		// load scores from playerprefs


		// update scores

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
