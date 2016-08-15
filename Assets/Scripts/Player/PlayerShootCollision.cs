using UnityEngine;
using System.Collections;

public class PlayerShootCollision : MonoBehaviour {

	[HideInInspector] public string[] targetTags = {"Enimigos","Obstacle"};
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col){
        /*foreach(string tag in targetTags) {
			Debug.Log(tag);
            if(tag == col.gameObject.tag) {
				if(tag=="Enimigos"){
                	GameManager.sharedInstance.changeScore(GameManager.sharedInstance.score + 1);
                	Destroy(col.gameObject);
				}
				Destroy(gameObject);
            }
        }*/

		//JEITO BURRO 
		if(col.gameObject.tag == "Enimigos") {
			Destroy(gameObject);
			col.gameObject.GetComponent<EnemyComponent>().hitpoints--;
			if(col.gameObject.GetComponent<EnemyComponent>().hitpoints <= 0) {
				GameManager.sharedInstance.changeScore(GameManager.sharedInstance.score + col.gameObject.GetComponent<EnemyComponent>().score);
				col.gameObject.GetComponent<EnemyComponent>().spawnPU();
				Destroy(col.gameObject);
			}

		}
		if(col.gameObject.tag == "Obstacle") {
			Destroy(gameObject);
		}
	}
}
