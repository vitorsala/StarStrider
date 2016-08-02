using UnityEngine;

public class LinearMovement : MonoBehaviour {

    [HideInInspector]
	public Vector3 magnitude;

	// Use this for initialization
	void Start () {
	}

	void FixedUpdate(){
		transform.Translate(magnitude * Time.fixedDeltaTime);
	}
}
