using UnityEngine;
using System.Collections;

public class PlayerShootComponent : MonoBehaviour {
	public GameObject shootObject;
	public float rateOfFire;
    public float projectileSpeed;

    public string[] targetTags = { "Enimigos" };

    public enum ShootType {
        Simple, Double, Spread
    }
    public ShootType type = ShootType.Simple;

    public float doubleShootSpaceFromMiddle = 0.4f;

	private double elapsedTime = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        if(rateOfFire == 0) return;

		elapsedTime += Time.deltaTime;
		if (elapsedTime >= rateOfFire) {

            float yOffset;
            GameObject shoot;

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            yOffset = transform.position.y + (renderer.bounds.size.y / 2) + 0.5f;

            switch (type) {
				case ShootType.Simple:

					shoot = Instantiate(shootObject, new Vector3(transform.position.x, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					shoot.GetComponent<LinearMovement>().magnitude = Vector3.up * projectileSpeed;
				
				//LUL
					shoot.transform.SetParent(gameObject.transform);

                break;

	            case ShootType.Double:

	                shoot = Instantiate(shootObject, new Vector3(transform.position.x - doubleShootSpaceFromMiddle, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					shoot.GetComponent<LinearMovement>().magnitude = Vector3.up * projectileSpeed;

	                shoot = Instantiate(shootObject, new Vector3(transform.position.x + doubleShootSpaceFromMiddle, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					shoot.GetComponent<LinearMovement>().magnitude = Vector3.up * projectileSpeed;

                break;

				case ShootType.Spread:
				
					//tiro do meio
					shoot = Instantiate(shootObject, new Vector3(transform.position.x, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					shoot.GetComponent<LinearMovement>().magnitude = Vector3.up * projectileSpeed;

					//tiros laterais
					shoot = Instantiate(shootObject, new Vector3(transform.position.x - doubleShootSpaceFromMiddle, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					Vector3 sideVector = new Vector3(-1, 2, 0);
					sideVector.Normalize();
					shoot.GetComponent<LinearMovement>().magnitude = sideVector * projectileSpeed;

					shoot = Instantiate(shootObject, new Vector3(transform.position.x + doubleShootSpaceFromMiddle, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<PlayerShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					Vector3 otherSideVector = new Vector3(1, 2, 0);
					otherSideVector.Normalize();
					shoot.GetComponent<LinearMovement>().magnitude = otherSideVector * projectileSpeed;


				break;

				

            }
		
			elapsedTime = 0;
		}
	}
}