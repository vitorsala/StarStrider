using UnityEngine;
using System.Collections;

public class PUWeaponEffect : PUEffect
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

		//TODO: weapon upgrades
		switch(GameManager.sharedInstance.player.GetComponent<PlayerShootComponent>().type){

			case(PlayerShootComponent.ShootType.Simple):
				GameManager.sharedInstance.player.GetComponent<PlayerShootComponent>().type = PlayerShootComponent.ShootType.Double;
				break;

			case(PlayerShootComponent.ShootType.Double):
				GameManager.sharedInstance.player.GetComponent<PlayerShootComponent>().type = PlayerShootComponent.ShootType.Spread;
				break;

			case(PlayerShootComponent.ShootType.Spread):
				GameManager.sharedInstance.player.GetComponent<PlayerShootComponent>().type = PlayerShootComponent.ShootType.DoubleSpread;
				break;

			case(PlayerShootComponent.ShootType.DoubleSpread):
				GameManager.sharedInstance.player.GetComponent<PlayerShootComponent>().type = PlayerShootComponent.ShootType.Railgun;
				break;

			default:
				return;
		}

	}
}

