using UnityEngine;

public class LinearMovement : MonoBehaviour {

    [HideInInspector]
	public Vector3 magnitude;

	// Use this for initialization
	void Start () {
	}

	void Update() {
        transform.Translate(magnitude * Time.deltaTime);
	}
}
