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
			Vector3 newPosition;
			float yoffset;
			if (gameObject.tag == "Player") {
				SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
				yoffset = transform.position.y + (renderer.bounds.size.y / 2) + 0.3f;
			}
			else {
				SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
				yoffset = transform.position.y - (renderer.bounds.size.y / 2) - 0.3f;
			}
			newPosition = new Vector3 (transform.position.x, yoffset, transform.position.z);
			GameObject shoot = Instantiate (shootObject, newPosition, transform.rotation) as GameObject;
			if (gameObject.tag == "Enimigos") {
				shoot.GetComponent<ShootCollision>().target = "Player";
				shoot.GetComponent<VerticalLinMovement>().magnitude = shoot.GetComponent<VerticalLinMovement> ().magnitude * -1;
			}
		}
	}
}