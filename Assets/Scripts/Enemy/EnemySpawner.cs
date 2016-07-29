using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyToSpawn;

    public BezierSpline enemyPath;
    public float enemySpeed;

    public float timeBetweenEntities;

    public int numberOfEnemies = 1;

	public float delay = 0;

	private float speed;
    private float elapsedTimeSinceLastSpawn = 0;
    private Vector3 origin;

	// Use this for initialization
	void Start () {
        origin = enemyPath.GetPoint(0f);
        elapsedTimeSinceLastSpawn = timeBetweenEntities;
		speed = GameManager.spawnerVelocity;
    }
	
	// Update is called once per frame
	void Update () {
		if (speed == 0) {
			if (delay <= 0) {
				elapsedTimeSinceLastSpawn += Time.deltaTime;
				if (elapsedTimeSinceLastSpawn >= timeBetweenEntities && numberOfEnemies > 0) {

					GameObject spawnedEnemy = GameObject.Instantiate (enemyToSpawn);
					spawnedEnemy.transform.position = origin;
					FollowSplinePath fsp = spawnedEnemy.GetComponent<FollowSplinePath> ();
					fsp.pathToFollow = enemyPath;
					fsp.speed = enemySpeed;

					elapsedTimeSinceLastSpawn = 0;
					numberOfEnemies--;
				}
			}
			else {
				delay -= Time.deltaTime;
			}
		}
		else {
			transform.Translate (0, -speed * Time.deltaTime, 0);
			if (transform.localPosition.y <= 0) {
				speed = 0;
			}
		}
	}
}
