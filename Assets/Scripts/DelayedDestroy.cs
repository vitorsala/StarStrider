using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {
	private float time = 0.3f;
	private float elapsedTime = 0;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(!GetComponent<SpriteRenderer>().isVisible) {
			elapsedTime += Time.deltaTime;
			if(elapsedTime >= time) {
				if(!GetComponent<SpriteRenderer>().isVisible) {
					Destroy(gameObject);
				}
			}
		}
		else {
			elapsedTime = 0;
		}


	}
}
