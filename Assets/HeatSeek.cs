using UnityEngine;
using System.Collections;

public class HeatSeek : MonoBehaviour {

	public GameObject targetGo;
	Vector3 targetPos;
	public bool launchRight;
	public float baseSpeed = 0.8f;
	float moveSpeed = 0.2f;
	float variance;
	System.Random rng;

	public float frequency = 1.0f;
	public float magnitude = 3.0f;   
	private Vector3 axis;

	// Use this for initialization 
	void Start () {
		rng = new System.Random();
		axis = transform.right;
		moveSpeed = baseSpeed;
		targetPos = new Vector3(rng.Next(-30, -10), 20);
		//variance = (float)rng.NextDouble()/3;
		variance = 1;
	}
		

	float secondCounter = 1.0f;
	float launchTimer = 0.8f;
	// Update is called once per frame
	void Update () {
		
		//Launch sequence
		if(launchTimer > -0.35f) {
			launchTimer -= Time.deltaTime;

			if(launchRight) {
				transform.Translate(transform.right * launchTimer * 0.25f * variance);
			} else {
				transform.Translate(-transform.right * launchTimer * 0.25f * variance);
			}
			transform.Translate(transform.up * 0.1f);

		} else {
			//Seek sequence
			secondCounter -= Time.deltaTime;

			moveSpeed *= 1.0015f;

			if(null != targetGo) {
				MoveTowards(targetGo, Time.deltaTime);
			}
			else {
				//MoveTowards();
				MoveTowards(targetPos);
			}

			//SINE MOVEMENT
			transform.Translate(axis * Mathf.Sin (Time.time * frequency) * magnitude);
		}
			
	}

	//TODO fazer movimentação melhor, baseada em curvas
	//o projetil vai traçar uma curva louca entre sua posição e o alvo, navegar ela por tipo 0.5s e ai calcular outra

	void MoveTowards(GameObject target, float timeElapsed){
		Vector3 direction = (target.transform.position - gameObject.transform.position).normalized;
		gameObject.transform.Translate(direction*moveSpeed);
	}

	void MoveTowards(Vector3 target){
		Vector3 direction = (target - gameObject.transform.position).normalized;
		gameObject.transform.Translate(direction*moveSpeed);
	}

	void MoveTowards(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enimigos");
		if(enemies.Length >= 1) {
			int targetNo = rng.Next(0, enemies.Length);
			targetGo = enemies[targetNo];
		} else {

			int xPos = rng.Next(-15, 16);
			Vector3 targetPos = new Vector3();
			targetPos.x = xPos;
			targetPos.y = 15;
			gameObject.transform.Translate(targetPos.normalized * moveSpeed);
		}
	}
}
