using UnityEngine;
using System.Collections;


public class EnemyShootComponent : MonoBehaviour {

    public enum ShootType {
        Simple, Line, Arc
    }

	public GameObject shootObject;
	public float rateOfFire;
    public float projectileSpeed;
    public float doubleShootSpaceFromMiddle = 0.4f;

    public string[] targetTags = { "Player" };

    public int numberOfShoot = 3;
    public bool[] arcShootEnabled = new bool[]{true, true, true};

    public ShootType type = ShootType.Simple;

	private double elapsedTime = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= rateOfFire) {
			elapsedTime = 0;
			float yOffset;

			SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
			yOffset = transform.position.y - (renderer.bounds.size.y / 2) - 0.5f;

			GameObject shoot;
			float step;

            switch (type) {
            case ShootType.Simple:

                shoot = Instantiate(shootObject, new Vector3(transform.position.x, yOffset, transform.position.z), transform.rotation) as GameObject;
                shoot.GetComponent<EnemyShootCollision>().targetTags = targetTags;
                shoot.AddComponent<LinearMovement>();
                shoot.GetComponent<LinearMovement>().magnitude = Vector3.down * projectileSpeed;
                break;

			case ShootType.Line:
				/*
                shoot = Instantiate(shootObject, new Vector3(transform.position.x - doubleShootSpaceFromMiddle, yOffset, transform.position.z), transform.rotation) as GameObject;
                shoot.GetComponent<EnemyShootCollision>().targetTags = targetTags;
                shoot.AddComponent<LinearMovement>();
                shoot.GetComponent<LinearMovement>().magnitude = Vector3.down * projectileSpeed;

                shoot = Instantiate(shootObject, new Vector3(transform.position.x + doubleShootSpaceFromMiddle, yOffset, transform.position.z), transform.rotation) as GameObject;
                shoot.GetComponent<EnemyShootCollision>().targetTags = targetTags;
                shoot.AddComponent<LinearMovement>();
                shoot.GetComponent<LinearMovement>().magnitude = Vector3.down * projectileSpeed;
				*/

				if (numberOfShoot > 1) {
					step = 1 / (numberOfShoot - 1);
					float t = 0;
					for (int i = 0; i < numberOfShoot; i++) {
						float pos = Mathf.Lerp(-0.5f, 0.5f, t);
						shoot = Instantiate(shootObject, new Vector3(transform.position.x + pos, yOffset, transform.position.z), transform.rotation) as GameObject;
						shoot.GetComponent<EnemyShootCollision>().targetTags = targetTags;
						shoot.AddComponent<LinearMovement>();
						shoot.GetComponent<LinearMovement>().magnitude = Vector3.down * projectileSpeed;
						t += step;
					}
				}
				else {
					shoot = Instantiate(shootObject, new Vector3(transform.position.x, yOffset, transform.position.z), transform.rotation) as GameObject;
					shoot.GetComponent<EnemyShootCollision>().targetTags = targetTags;
					shoot.AddComponent<LinearMovement>();
					shoot.GetComponent<LinearMovement>().magnitude = Vector3.down * projectileSpeed;
				}
					
                break;

            case ShootType.Arc:
                float x,y;
                float r = 1f;

                step = Mathf.PI / (numberOfShoot+1);
                float ang = Mathf.PI + step;

                for(int i = 1; i <= numberOfShoot; i++){
                    x = transform.position.x + (Mathf.Cos(ang) * r);
                    y = transform.position.y + (Mathf.Sin(ang) * r);

                    Vector3 pos = new Vector3(x,y,0);
                    Vector3 dir = (pos - transform.position).normalized;

                    shoot = Instantiate(shootObject, pos, Quaternion.identity) as GameObject;

                    shoot.GetComponent<EnemyShootCollision>().targetTags = targetTags;
                    shoot.AddComponent<LinearMovement>();
                    shoot.GetComponent<LinearMovement>().magnitude = dir * projectileSpeed;

                    ang += step;
                }
                break;
            }
		}
	}
}