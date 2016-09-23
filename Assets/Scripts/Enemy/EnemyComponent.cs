using UnityEngine;
using System.Collections;

public class EnemyComponent : MonoBehaviour
{

	public int hitpoints;
	public int score;

	public GameObject puObject;
	public GameObject explosionParticles;

	public enum PuType{
		None, Score, Special, Weapon
	}
	public PuType type = PuType.None;

	[Tooltip("Chance de 0 a 1 de gerar PowerUp ao destruir o inimigo.")]
	[Range(0,1)]
	public float puChance;

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
	
	}

	private bool isQuitting = false;
	void OnApplicationQuit(){
		isQuitting = true;
	}

	public void TakePlayerDamage(int damage){
		hitpoints -= damage;
		if(hitpoints <= 0) {
			GameManager.sharedInstance.changeScore(GameManager.sharedInstance.score + score);
			spawnPU();
			//gameObject.GetComponent<ParticleSystem>().Play();
			GameObject particleGo = Instantiate(explosionParticles);
			particleGo.transform.position = gameObject.transform.position;
			Destroy(gameObject);
			particleGo.GetComponent<ParticleSystem>().Play();
			Destroy(particleGo, particleGo.GetComponent<ParticleSystem>().duration);
		}
	}

	public void spawnPU(){

		//Teste maroto
		if(!isQuitting) {
			if(type != PuType.None && puChance > 0) {
				float roll = Random.value;
				if(roll <= puChance) {
					//Spawn PowerUp
					GameObject pu;
					switch(type) {
						case PuType.Score:
							pu = Instantiate(puObject, transform.position, transform.rotation) as GameObject;
							pu.AddComponent<PUScoreEffect>();
							break;

						case PuType.Weapon:
							pu = Instantiate(puObject, transform.position, transform.rotation) as GameObject;
							pu.AddComponent<PUWeaponEffect>();
							break;

						case PuType.Special:
							pu = Instantiate(puObject, transform.position, transform.rotation) as GameObject;
							pu.AddComponent<PUSpecialEffect>();
							break;
					}

				}
			}
		}

	}
}

