using UnityEngine;
using System.Collections;

public class PUCollision : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnTriggerEnter2D (Collider2D col){
		
		//JEITO BURRO 
		if(col.gameObject.tag == "Player") {
			//GET POWERUP
			gameObject.GetComponent<PUEffect>().OnCollect();
			Destroy(gameObject);
		}
	}
}

