  using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class EnemySpawner : MonoBehaviour {

	[HideInInspector] public Wave data;

	// Use this for initialization
	void Start () {
		if(data.enemyPath == null)   Destroy(gameObject);

		data.origin = data.enemyPath.GetPoint(0);

		data.elapsedTimeSinceLastSpawn = data.timeBetweenEntities;
    }
	
	// Update is called once per frame
	void Update () {
		if((data.numberOfEnemies == 0 && transform.childCount == 0) || !data.active){
            Destroy(gameObject);
        }
		else if (data.timeToStartSpawner <= 0) {
			data.elapsedTimeSinceLastSpawn += Time.deltaTime;
			if (data.elapsedTimeSinceLastSpawn >= data.timeBetweenEntities && data.numberOfEnemies > 0) {

				GameObject spawnedEnemy = GameObject.Instantiate (data.enemyToSpawn);
				spawnedEnemy.transform.SetParent(gameObject.transform);
				spawnedEnemy.transform.position = data.origin;
				FollowSplinePath fsp = spawnedEnemy.GetComponent<FollowSplinePath> ();
				fsp.pathToFollow = data.enemyPath;
				fsp.speed = data.enemySpeed;
                EnemyShootComponent enemyShootComponent = spawnedEnemy.GetComponent<EnemyShootComponent>();
				enemyShootComponent.type = data.shootType;
				enemyShootComponent.numberOfShoot = data.numberOfShoot;
				enemyShootComponent.projectileSpeed = data.projectileSpeed;

				data.elapsedTimeSinceLastSpawn = 0;
				data.numberOfEnemies--;
			}
		}
		else {
			data.timeToStartSpawner -= Time.deltaTime;
		}
	}
}
