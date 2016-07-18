using UnityEngine;
using System.Collections;

public class BackgroundLoopSystem : MonoBehaviour {

	public float scrollSpeed;
	public float amount;

	private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		this.startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.timeSinceLevelLoad * scrollSpeed, amount);
		transform.position = startPosition + Vector3.down * newPosition;
	}
}
