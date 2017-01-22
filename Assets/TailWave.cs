using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWave : MonoBehaviour {
	private int size;
	private Vector3 [] vectors;
	private float [] xValues;
	private LineRenderer lineRenderer;
	private float minX;
	private float maxX;
	private float gap;

    private List<float> history;
    private GameObject ball;
    private float ballY;

	void Start () {
        ball = GameObject.FindWithTag("Player");
        ballY = ball.transform.position.y;
        history = new List<float>();

        GameObject targetWave = GameObject.FindWithTag("wave");
		Wave targetWaveScript = targetWave.GetComponent<Wave>();

        size = Wave.size / 2;

        vectors = new Vector3 [size];
        xValues = new float [size];

		lineRenderer = this.gameObject.GetComponent <LineRenderer> ();
		lineRenderer.numPositions = size;
		lineRenderer.startWidth = 0.1f;
		lineRenderer.endWidth = 0.1f;
		minX = -Camera.main.orthographicSize * Camera.main.aspect;
		float currentX = minX;
		gap = (- minX) / size;
		for (int i = 0; i < size; i++) {
            history.Add(ballY);

			vectors [i] = new Vector3 (currentX, ballY, 0);
			xValues [i] = currentX;
			currentX += gap;
		}
	}

	void Update () {
        ballY = ball.transform.position.y;

        // cycle the new position in history
        history.Add(ballY);
        history.RemoveAt(0);


		for (int i = 0; i < size; i++) {
			vectors [i] = new Vector3 (xValues[i], history[i], 0);
		}
		lineRenderer.SetPositions (vectors);
	}

}
