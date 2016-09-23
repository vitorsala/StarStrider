using UnityEngine;
using System.Collections;

public class PUSpecialEffect : PUEffect
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public override void OnCollect(){

		GameManager.sharedInstance.AddSpecAmmo(1);

	}
}

