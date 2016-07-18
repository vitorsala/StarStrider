using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {
	//public float time;
	//private float elapsedTime = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//elapsedTime += Time.deltaTime;
		//if (elapsedTime >= time) {
		//	Destroy (gameObject);
		//}

		if (!GetComponent<SpriteRenderer>().isVisible) {
			Destroy (gameObject);
		}
	}
}
