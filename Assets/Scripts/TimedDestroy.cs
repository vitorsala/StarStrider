using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {
	public float time = 0.3f;
	private float elapsedTime = 0;

	private Color start;
	private Color end;
	private float t = 0.0f;

	void Start () {
		start = gameObject.GetComponent<SpriteRenderer>().material.color;
		end = new Color(start.r, start.g, start.b, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		gameObject.GetComponent<SpriteRenderer>().material.color = Color.Lerp(start, end, t/time);

		elapsedTime += Time.deltaTime;
		if(elapsedTime >= time) {
			elapsedTime = -10000;
			Destroy(gameObject);
		}
	}

	void FuckOff(){
		Destroy(gameObject);
	}
}
