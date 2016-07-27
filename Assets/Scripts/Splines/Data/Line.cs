using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	public Vector3 p0, p1;

	void Reset(){
		p0 = new Vector3(1f,0f,0f);
		p1 = new Vector3(1f,1f,0f);
	}
}
