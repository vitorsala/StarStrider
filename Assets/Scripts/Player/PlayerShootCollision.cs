using UnityEngine;
using System.Collections;

public class PlayerShootCollision : MonoBehaviour {

	[HideInInspector] public string[] targetTags = {"Enimigos","Obstacle"};
	[HideInInspector] public int damage = 1;
	public bool isFixed = false;


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
			if(!isFixed)
				Destroy(gameObject);
			col.gameObject.GetComponent<EnemyComponent>().TakePlayerDamage(damage);
		}
		if(col.gameObject.tag == "Obstacle") {
			if(!isFixed)
				Destroy(gameObject);
		}
	}
}
