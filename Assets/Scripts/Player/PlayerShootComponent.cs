using UnityEngine;
using System.Collections;

public class PlayerShootComponent : MonoBehaviour {
	public GameObject shootObject;
	public float rateOfFire;
    public float projectileSpeed;

    public string[] targetTags = { "Enimigos" };

    public enum ShootType {
        Simple, Double
    }
    public ShootType type = ShootType.Simple;

	private double elapsedTime = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= rateOfFire) {

            switch (type) {

                case ShootType.Simple:
                    
			        Vector3 newPosition;
			        float yoffset;

			        SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
			        yoffset = transform.position.y + (renderer.bounds.size.y / 2) + 0.5f;

			        newPosition = new Vector3 (transform.position.x, yoffset, transform.position.z);

			        GameObject shoot = Instantiate (shootObject, newPosition, transform.rotation) as GameObject;
                    shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
                    shoot.GetComponent<VerticalLinMovement>().magnitude = projectileSpeed;

                    break;
                case ShootType.Double:


                    break;
            }

			elapsedTime = 0;
		}
	}
}