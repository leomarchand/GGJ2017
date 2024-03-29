﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {

	public Text name1Text, name2Text, name3Text, name4Text, name5Text;
	public Text score1Text, score2Text, score3Text, score4Text, score5Text;


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
				PlayerPrefs.GetString(i.ToString() + "name", "towelboy"),
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

		name1Text.text = scoreList[0].name;
		name2Text.text = scoreList[1].name;
		name3Text.text = scoreList[2].name;
		name4Text.text = scoreList[3].name;
		name5Text.text = scoreList[4].name;

		score1Text.text = scoreList[0].score.ToString() + " -";
		score2Text.text = scoreList[1].score.ToString() + " -";
		score3Text.text = scoreList[2].score.ToString() + " -";
		score4Text.text = scoreList[3].score.ToString() + " -";
		score5Text.text = scoreList[4].score.ToString() + " -";


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("mainmenu");
		}
	}


}
