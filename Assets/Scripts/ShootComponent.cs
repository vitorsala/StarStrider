using UnityEngine;
using System.Collections;

public class ShootComponent : MonoBehaviour {
	public GameObject shootObject;
	public float timeToSpawn;
	private double elapsedTime = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= timeToSpawn) {
			elapsedTime = 0;
			Instantiate (shootObject, transform.position, transform.rotation);
		}
	}


}