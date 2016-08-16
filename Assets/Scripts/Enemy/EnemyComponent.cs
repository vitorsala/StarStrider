using UnityEngine;
using System.Collections;

public class EnemyComponent : MonoBehaviour
{

	public int hitpoints;
	public int score;

	public GameObject puObject;

	public enum PuType{
		None, Score, Special, Weapon
	}
	public PuType type = PuType.None;

	[Tooltip("Chance de 0 a 1 de gerar PowerUp ao destruir o inimigo.")]
	[Range(0,1)]
	public float puChance;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	private bool isQuitting = false;
	void OnApplicationQuit(){
		isQuitting = true;
	}

	public void spawnPU(){

		//Teste maroto
		if(!isQuitting) {
			if(type != PuType.None && puChance > 0) {
				float roll = Random.value;
				if(roll <= puChance) {
					//Spawn PowerUp
					GameObject lul = Instantiate(puObject, transform.position, transform.rotation) as GameObject;

				}
			}
		}

	}
}

