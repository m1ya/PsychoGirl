using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public GameObject gameOverCanvas;
	public GameObject panelCanvas;
	public GameObject canvas;
	public Text score;
	public AudioSource walk;
	public AudioSource dead;
	public AudioSource bgm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.gameObject.tag == "Enemy") {
			StartCoroutine(Dead(col));
		}
	}

	IEnumerator Dead(Collision col){
		PlayerLevel.isGameOver = true;
		bgm.Stop ();
		dead.Play ();
		walk.Stop ();
		gameObject.transform.FindChild("unitychan").gameObject.GetComponent<Animator> ().SetBool ("isRunning", false);
		panelCanvas.SetActive (true);
		yield return new WaitForSeconds (1f);
		col.gameObject.SetActive (false);
		gameOverCanvas.SetActive (true);
		canvas.SetActive (false);
		score.text = "Score : " + ScoreManager.score.ToString("#");
		Debug.Log ("GameOver");
	}
}
