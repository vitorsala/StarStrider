using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		//transform.position = Vector2.Lerp (transform.position, mousePosition, moveSpeed);
		Vector3 movementVector = mousePosition - transform.position;
		movementVector.z = 0;
		movementVector.Normalize ();
		transform.Translate (movementVector * moveSpeed);
	}
}
