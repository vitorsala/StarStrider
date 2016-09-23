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
			if(!col.gameObject.GetComponent<PlayerComponent>().isInvul) {
				GameManager.sharedInstance.changeLives(GameManager.sharedInstance.life - 1);
				col.gameObject.GetComponent<PlayerComponent>().damageInvul();
			}
		}
		if(col.gameObject.tag == "Enimigos") {
			Destroy(col.gameObject);
		}
	}
}

