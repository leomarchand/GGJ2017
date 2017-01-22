using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameEntry : MonoBehaviour {

	InputField inputField;
	public static string output;

	void Start () {
		inputField = gameObject.GetComponent<InputField>();
		inputField.onEndEdit.AddListener(delegate{SubmitInput();});
	}
	
	private void SubmitInput() {
		output = inputField.text.ToString();
		inputField.text = "";
		SceneManager.LoadScene ("scoreboard");
	}
}
