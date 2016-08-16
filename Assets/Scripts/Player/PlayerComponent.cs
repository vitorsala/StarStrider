using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour
{
	[HideInInspector]
	public bool isInvul = true;
	private float invulTimer;

	public float defaultInvulTimer = 0.5f;

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
		if(isInvul) {
			if(invulTimer <= 0) {
				isInvul = false;
				GetComponent<Animator>().SetBool("playerIsInvul", false);
			}
			else {
				invulTimer -= Time.deltaTime;
			}
		}
	}

	public void damageInvul(){
		isInvul = true;
		GetComponent<Animator>().SetBool("playerIsInvul", true);
		invulTimer = defaultInvulTimer;
		//resetPosition();
	}

	void resetPosition(){
		transform.localPosition = GameManager.sharedInstance.startingPoint;
	}
}

