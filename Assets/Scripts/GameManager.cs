using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager sharedInstance;

	public enum GameState {Starting, Playing, Death, GameOver, BeatLevel};
	public GameState gameState = GameState.Starting;

	public Vector3 startingPoint;

	//
	[Tooltip("Referência para o jogador. Caso NULL, será o GameObject com a tag \"Player\".")]
	public GameObject player;

	public int score = 0;
	public int life = 1;
	public int bonusLifeScore = 1000;
	private int scoreSinceLife = 0;
	
	//
	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public Text livesDisplay;

	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;

	//
	public static float spawnerVelocity = 5f;


	private float timer;


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

		livesDisplay.text = "Lives: " + GameManager.sharedInstance.life;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Elapsed Time: " + Time.timeSinceLevelLoad.ToString());

		switch (gameState) {
		case GameState.Starting:

		    if (player.transform.localPosition != startingPoint) {
			    float step = 0.8f * Time.deltaTime;
			    player.transform.localPosition = Vector3.MoveTowards (player.transform.localPosition, startingPoint, step);
		    }
		    else {
				player.GetComponent<PlayerComponent>().isInvul = false;
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerShootComponent>().enabled = true;
			    gameState = GameState.Playing;
		    }
		    break;
		case GameState.Playing:
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
		scoreSinceLife += newScore - score;
		score = newScore;
		mainScoreDisplay.text = "Score: "+score;
		gameOverScoreDisplay.text = mainScoreDisplay.text;

		//handling de ganhar vidas
		if(scoreSinceLife >= bonusLifeScore) {
			scoreSinceLife -= bonusLifeScore;
			life++;

			//TODO: efeitos de ganhar vida extra
		}
	}

}
