using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typer : MonoBehaviour {

	public float freq;
	public Text freq_text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			transform.position += new Vector3(0, 0.1f, 0);
		}

		Debug.Log(freq_text.text);

		
	}
}
