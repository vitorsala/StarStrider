using UnityEngine;
using System.Collections;

public class ObstacleCollision : MonoBehaviour
{

	[HideInInspector] public string[] targetTags = {"Player"};
	// Update is called once per frame
	void Update(){
	
	}

	void OnTriggerEnter2D(Collider2D col){
		/*foreach(string tag in targetTags) {
			if(tag == col.gameObject.tag) {
				GameManager.sharedInstance.life--;
			}
		}*/

		//JEITO BURRO

		if(col.gameObject.tag == "Player") {
			GameManager.sharedInstance.life--;
		}
		if(col.gameObject.tag == "Enimigos") {
			Destroy(col.gameObject);
		}
	}
}

