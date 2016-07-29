using UnityEngine;
using System.Collections;

public class PlayerShootCollision : MonoBehaviour {

	[HideInInspector] public string[] targetTags = {"Enimigos"};
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col){
        foreach(string tag in targetTags) {
            if(tag == col.gameObject.tag) {
                // Fazer a lógica da colisão
                GameManager.sharedInstance.changeScore(GameManager.sharedInstance.score + 1);
                Destroy(col.gameObject);
                Destroy(gameObject);
            }
        }
	}
}
