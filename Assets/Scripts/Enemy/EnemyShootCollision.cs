using UnityEngine;
using System.Collections;

public class EnemyShootCollision : MonoBehaviour {

	[HideInInspector] public string[] targetTags = {"Player"};
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col){
        /*foreach(string tag in targetTags) {
            if(tag == col.gameObject.tag) {
                // Fazer a lógica da colisão

                GameManager.sharedInstance.life--;
            }
        }*/

		//JEITO BURRO 
		//atingir jogador
		if(col.gameObject.tag == "Player") {
			if(!col.gameObject.GetComponent<PlayerComponent>().isInvul) {
				GameManager.sharedInstance.changeLives(GameManager.sharedInstance.life - 1);
				col.gameObject.GetComponent<PlayerComponent>().damageInvul();
			}
			Destroy(gameObject);
		}
		//atingir cenario
		if(col.gameObject.tag == "Obstacle") {
			Destroy(gameObject);
		}
		//atingir escudos
		if(col.gameObject.tag == "Shield") {
			Destroy(gameObject);
		}
	}
}
