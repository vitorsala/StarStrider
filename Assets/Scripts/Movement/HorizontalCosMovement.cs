using UnityEngine;
using System.Collections;

public class HorizontalCosMovement : MonoBehaviour {
	
	public float magnitude;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.right * Mathf.Cos(Time.timeSinceLevelLoad) * magnitude);
	}
}
