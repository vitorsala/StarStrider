using UnityEngine;
using System.Collections;

public class SwarmSpec : PlayerSpecialComponent
{

	bool isActive = false;

	public int swarmSize = 55;
	public int missileDamage = 1;
	float fireDelay = 0.01f;
	float currentDelay = 0.1f;

	int remainingMissiles = 55;

	System.Random rng;


	// Use this for initialization
	void Start()
	{
		rng = new System.Random();
		currentDelay = fireDelay;
	}

	// Update is called once per frame
	void Update(){
		
		if(isActive) {
			currentDelay -= Time.deltaTime;
			if(remainingMissiles <= 0) {
				EndSpecial();
			} else {
				if(currentDelay <= 0) {
					currentDelay = fireDelay;

					//FIRE
					GameObject missile = Instantiate(Resources.Load("Missile", typeof(GameObject)) as GameObject, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
					missile.transform.parent = GameManager.sharedInstance.transform;
					GameObject targetEnemy = FindTarget();

					missile.GetComponent<HeatSeek>().targetGo = FindTarget();
					missile.GetComponent<HeatSeek>().launchRight = (remainingMissiles % 2 == 0);
					missile.GetComponent<PlayerShootCollision>().damage = missileDamage;

					remainingMissiles--;
				}
			}
		}
	}

	public override void ActivateSpecial(){

		isActive = true;

	}

	public GameObject FindTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enimigos");
		if(enemies.Length >= 1) {
			int targetNo = rng.Next(0, enemies.Length);
			return enemies[targetNo];
		}
		return null;
	}

	public override void EndSpecial(){
		isActive = false;
		remainingMissiles = swarmSize;
		currentDelay = fireDelay;
	}
}

