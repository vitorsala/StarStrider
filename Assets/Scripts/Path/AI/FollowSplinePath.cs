using UnityEngine;
using System.Collections;

public class FollowSplinePath : MonoBehaviour {

	[HideInInspector] public Path pathToFollow;

    [HideInInspector] public float speed = 1;

    [HideInInspector] public bool ignoreDelay = false;

    private float t = 0;
    private float delayTime = 0;
    private int segmentNumber;

	void Start () {
        segmentNumber = -1;
	}
	
	void FixedUpdate () {
		if(pathToFollow != null) {
            if(delayTime <= 0 || ignoreDelay) {
                t += Time.fixedDeltaTime * speed;

                if(t >= 1f) {
                    //delay entre segmento
                    //zerar o deltatime
                    if(pathToFollow.loop) {
                        t = 0;
                    }
                    else {
                        Destroy(gameObject);
                        t = 1;
                    }
                }

                PointData point = pathToFollow.GetPoint(t);
                transform.position = point.point;
                if(segmentNumber != point.segmentIndex) {
                    segmentNumber = point.segmentIndex;
                    delayTime = point.delay;
                }
            }
            else {
                delayTime -= Time.fixedDeltaTime;
            }
		}   
	}
}
