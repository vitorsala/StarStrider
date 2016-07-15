using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager sharedInstance;

	public enum GameState {Starting, Playing, Death, GameOver, BeatLevel};
	public GameState gameState = GameState.Starting;

	//
	[Tooltip("Referência para o jogador. Caso NULL, será o GameObject com a tag \"Player\".")]
	public GameObject player;

	public int score = 0;
	public int life = 1;

	//
	public GameObject mainCanvas;
	public Text mainScoreDisplay;

	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;


	public 
	// Use this for initialization
	void Start () {
		if (sharedInstance == null) {
			sharedInstance = gameObject.GetComponent<GameManager> ();
		}
		if (player == null) {
			player = GameObject.FindWithTag ("Player");
		}

		mainCanvas.SetActive (true);
		gameOverCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState) {
		case GameState.Starting:

			gameState = GameState.Playing;
			break;
		case GameState.Playing:
			Debug.Log (life);
			if (life <= 0) {
				gameState = GameState.Death;
				player.SetActive (false);
			}
			break;
		case GameState.Death:
			gameState = GameState.GameOver;
			break;
		case GameState.GameOver:
			mainCanvas.SetActive (false);
			gameOverCanvas.SetActive (true);
			break;
		}
	}

	public void changeScore(int newScore){
		score = newScore;
		mainScoreDisplay.text = "Score: "+score;
		gameOverScoreDisplay.text = mainScoreDisplay.text;
	}

}
