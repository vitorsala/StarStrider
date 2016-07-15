using UnityEngine;
using System.Collections;

public class ShootCollision : MonoBehaviour {
	public string target = "Enimigos";
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == target) {
			if (target == "Enimigos") {
				GameManager.sharedInstance.changeScore (GameManager.sharedInstance.score + 1);
				Destroy (col.gameObject);
				Destroy (gameObject);
			} else if (target == "Player") {
				GameManager.sharedInstance.life --;
			}
		}
	}
}
