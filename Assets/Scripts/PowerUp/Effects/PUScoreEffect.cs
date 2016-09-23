using UnityEngine;
using System.Collections;

public class PUScoreEffect : PUEffect
{

	public int scoreAmount = 100;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public override void OnCollect(){
		GameManager.sharedInstance.ChangeScoreBy(scoreAmount);
	}
}

