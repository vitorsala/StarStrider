using UnityEngine;

public class VerticalLinMovement : MonoBehaviour {

    [HideInInspector]
    public float magnitude;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.up * magnitude);
	}
}
