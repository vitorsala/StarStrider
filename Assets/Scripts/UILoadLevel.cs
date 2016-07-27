using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UILoadLevel : MonoBehaviour {
	public string sceneToLoad;

	public void loadLevel() {
		Debug.Log ("opa!");
		SceneManager.LoadScene (sceneToLoad);
	}
}