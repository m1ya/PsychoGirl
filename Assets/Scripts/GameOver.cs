using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public GameObject gameOverCanvas;
	public GameObject canvas;
	public Text score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.gameObject.tag == "Enemy") {
			PlayerLevel.isGameOver = true;
			gameOverCanvas.SetActive (true);
			canvas.SetActive (false);
			score.text = "Score : " + ScoreManager.score.ToString("#");
			Debug.Log ("GameOver");
		}
	}
}
