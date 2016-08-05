using UnityEngine;
using System.Collections;

public class EnemySpawnerManager : MonoBehaviour {
	
	public GameObject spawnerPrefab;

	[HideInInspector] LevelData waveData;

	// Use this for initialization
	void Start () {
		waveData = GetComponent<LevelData>(); // Busca pelo componente que contém os dados das waves.
		// Se o componente não exisitr, não faz nada.
		if (waveData == null) {
			return;
		}
		else {
			GameObject spawner;
			foreach (Wave wave in waveData.waves) {
				
				// Instancia um novo spawner, e adiciona como filho de Game Manager.
				spawner = Instantiate(spawnerPrefab, gameObject.transform) as GameObject;

				// Garante que o spawner vai estar na origem da cena, pois caso não esteja, pode bugar o path.
				spawner.transform.localPosition = Vector3.zero;

				// Pega o componente do spawner
				EnemySpawner es = spawner.GetComponent<EnemySpawner>();
				es.data = wave;
			}
		}
	}
}
