using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
	public static int size = 50;
	private Vector3 [] vectors = new Vector3 [size];
	private float [] xValues = new float [size];
	private LineRenderer lineRenderer;
	private float offset = 0f;
	private float minX;
	private float maxX;
	private float gap;
	public static float minFreq;
	public static float maxFreq;
	public static float multiplier;

	void Start () {
		lineRenderer = this.gameObject.GetComponent <LineRenderer> ();
		lineRenderer.numPositions = size;
		lineRenderer.startWidth = 0.1f;
		lineRenderer.endWidth = 0.1f;
		minX = -Camera.main.orthographicSize * Camera.main.aspect;
		maxX = -minX;
		float currentX = minX;
		gap = (maxX - minX) / size;
		for (int i = 0; i < size; i++) {
			float currentY = getYForX (currentX);
			vectors [i] = new Vector3 (currentX, currentY, 0);
			xValues [i] = currentX;
			currentX += gap;
		}
	}

	void Update () {
		for (int i = 0; i < size; i++) {
			float currentY = getYForX (xValues[i]);
			vectors [i] = new Vector3 (xValues[i], currentY, 0);
		}
		offset += gap;
		lineRenderer.SetPositions (vectors);
	}

	public float getYForX (float x) {
		return Mathf.Sin (multiplier*(x + offset));
	}
}
