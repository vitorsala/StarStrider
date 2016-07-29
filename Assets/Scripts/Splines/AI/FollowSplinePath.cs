using UnityEngine;
using System.Collections;

public class FollowSplinePath : MonoBehaviour {

    [HideInInspector] public BezierSpline pathToFollow;

    [HideInInspector] public float speed = 1;

    private float t = 0;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(pathToFollow != null) {
            t += Time.deltaTime * speed;
            if (t >= 1f) {
                if (pathToFollow.loop) {
                    t = 0;
                }
                else {
                    Destroy(gameObject);
                    t = 1;
                }
            }
            transform.position = pathToFollow.GetPoint(t);
        }
	}
}
