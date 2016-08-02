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

    public float doubleShootSpaceFromMiddle = 0.4f;

	private double elapsedTime = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= rateOfFire) {

            float yoffset;
            GameObject shoot;

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            yoffset = transform.position.y + (renderer.bounds.size.y / 2) + 0.5f;

            switch (type) {
            case ShootType.Simple:

                shoot = Instantiate(shootObject, new Vector3(transform.position.x, yoffset, transform.position.z), transform.rotation) as GameObject;
                shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
                shoot.AddComponent<VerticalLinMovement>();
                shoot.GetComponent<VerticalLinMovement>().magnitude = projectileSpeed;
                break;

            case ShootType.Double:

                shoot = Instantiate(shootObject, new Vector3(transform.position.x - doubleShootSpaceFromMiddle, yoffset, transform.position.z), transform.rotation) as GameObject;
                shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
                shoot.AddComponent<VerticalLinMovement>();
                shoot.GetComponent<VerticalLinMovement>().magnitude = projectileSpeed;

                shoot = Instantiate(shootObject, new Vector3(transform.position.x + doubleShootSpaceFromMiddle, yoffset, transform.position.z), transform.rotation) as GameObject;
                shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
                shoot.AddComponent<VerticalLinMovement>();
                shoot.GetComponent<VerticalLinMovement>().magnitude = projectileSpeed;

                break;
            }
            
			elapsedTime = 0;
		}
	}
}