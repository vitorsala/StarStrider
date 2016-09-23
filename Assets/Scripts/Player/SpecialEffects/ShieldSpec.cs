using UnityEngine;
using System.Collections;

public class ShieldSpec : PlayerSpecialComponent
{

	public float baseDuration = 5.0f;
	float shieldDuration = 5.0f;
	bool isActive = false;

	private GameObject shield;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(isActive && shieldDuration <= 0) {
			EndSpecial();
		} else if(isActive) {
			shieldDuration -= Time.deltaTime;
		} else {
			shieldDuration = baseDuration;
		}
	}

	public override void ActivateSpecial(){
		if(!isActive) {
			shield = Instantiate(Resources.Load("Shield", typeof(GameObject)) as GameObject, gameObject.transform) as GameObject;
			shield.transform.position = gameObject.transform.position;
			isActive = true;
		}
	}

	public override void EndSpecial(){
		if(null != shield) {
			isActive = false;
			Destroy(shield);
		}
	}
}