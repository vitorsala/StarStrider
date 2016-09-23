using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 0.5f;

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
        movementVector.y = 0;

		if (movementVector.magnitude > 1) {
			movementVector.Normalize ();
		}
		transform.Translate (movementVector * moveSpeed * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.Space)) {
			//SPECIAL
			if(GameManager.sharedInstance.specAmmo > 0) {
				gameObject.GetComponent<PlayerSpecialComponent>().ActivateSpecial();
				GameManager.sharedInstance.RemoveSpecAmmo(1);
			}else {
				//no ammo event 
			}
		}
	}
}
