using UnityEngine;
using System.Collections;

public class BombSpec : PlayerSpecialComponent
{

	public float explosionRadius = 10.0f;
	public int damage = 50;
	bool isActive = false;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public override void ActivateSpecial(){
		GameObject boom = Instantiate(Resources.Load("BOOM", typeof(GameObject)) as GameObject, gameObject.transform) as GameObject;
		boom.transform.position = gameObject.transform.position;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enimigos");
		foreach(GameObject enemy in enemies){
			if(Vector3.Distance(enemy.transform.position, gameObject.transform.position) <= explosionRadius) {
				enemy.GetComponent<EnemyComponent>().TakePlayerDamage(damage);
			}
		}

	}

	public override void EndSpecial(){
		isActive = false;
	}
}

