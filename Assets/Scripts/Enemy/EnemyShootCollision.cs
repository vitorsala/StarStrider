using UnityEngine;
using System.Collections;

public class EnemyShootCollision : MonoBehaviour {

	[HideInInspector] public string[] targetTags = {"Player"};
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col){
        foreach(string tag in targetTags) {
            if(tag == col.gameObject.tag) {
                // Fazer a lógica da colisão

                GameManager.sharedInstance.life--;
            }
        }
	}
}
