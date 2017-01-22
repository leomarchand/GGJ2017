using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

//		scoreList.Add(new highscore("MACHO", 69));
//		scoreList.Add(new highscore("MJ KIM", 68));
//
//		Debug.Log(scoreList[0].name);
//		Debug.Log(scoreList[1].score);
//

		// load scores from playerprefs

		scoreList = new List<highscore>();

		for (int i = 0; i < 5; i++) {
			scoreList.Add(new highscore(
				PlayerPrefs.GetString(i.ToString() + "name", ""),
				PlayerPrefs.GetInt(i.ToString() + "score", 0)));
		}

		// update scores
		string playerName = NameEntry.output;
		int playerScore = Level.levelNum;
		int playerIndex = 5;
		for (int i = 0; i < 5; i++) {
			if (playerScore > scoreList[i].score) {
				playerIndex = i;
				break;
			}
		}
		if (playerIndex < 5) {
			for (int i = 3; i >= playerIndex; i--) {
				scoreList[i+1] = scoreList[i];
			}
			scoreList[playerIndex] = new highscore(playerName, playerScore);
			for (int i = 0; i < scoreList.Count; i++) {

				PlayerPrefs.SetString(i.ToString() + "name", scoreList[i].name);
				PlayerPrefs.SetInt(i.ToString() + "score", scoreList[i].score);

			}

			PlayerPrefs.Save(); 
		}

		firstText.text = interfaceTextForScore(scoreList[0]);
		secondText.text = interfaceTextForScore(scoreList[1]);
		thirdText.text = interfaceTextForScore(scoreList[2]);
		fourthText.text = interfaceTextForScore(scoreList[3]);
		fifthText.text = interfaceTextForScore(scoreList[4]);


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("mainmenu");
		}
	}

	string interfaceTextForScore(highscore score) {
		return score.name + " - " + score.score.ToString();
	}
}
