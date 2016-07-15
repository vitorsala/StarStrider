using UnityEngine;
using System.Collections;

public class ShootCollisionComponent : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.gameObject.tag == "Enimigos"){
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
