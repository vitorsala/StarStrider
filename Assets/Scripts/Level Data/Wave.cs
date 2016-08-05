using System;
using UnityEngine;

[Serializable]
public struct Wave {
	public enum WaveType{
		Enemy, Obstacle
	}

	public bool active;
	public float enemySpeed;
	public float timeBetweenEntities;
	public int numberOfEnemies;
	public float timeToStartSpawner;
	public float elapsedTimeSinceLastSpawn;
	public int numberOfShoot;
	public float projectileSpeed;

	public Path enemyPath;
	public WaveType waveType;
	public GameObject enemyToSpawn;
	public Vector3 origin;
	public EnemyShootComponent.ShootType shootType;
}

