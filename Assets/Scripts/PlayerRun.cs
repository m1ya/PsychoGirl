using UnityEngine;
using System.Collections;

public class PlayerRun : MonoBehaviour {

	float scoreWall = 0;
	public AudioSource psy;
	public AudioSource walk;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerLevel.isGameOver || !PlayerLevel.isStart)	return;

		transform.position += Vector3.forward * Time.deltaTime * PlayerLevel.speed;
		ScoreManager.score += PlayerLevel.speed;
		if(PlayerLevel.speed < 9.9){
			if (ScoreManager.score - scoreWall >= 1000) {
				gameObject.transform.FindChild ("unitychan").gameObject.GetComponent<Animator> ().speed += 0.2f;
				if (walk.pitch < 3.0f) {
					walk.pitch += 0.2f;
				}
				PlayerLevel.speed += 0.2f;
				Debug.Log (PlayerLevel.speed);
				scoreWall = ScoreManager.score;
				if (PlayerLevel.NeedTime > 1) {
					psy.pitch -= 0.033f;
					PlayerLevel.NeedTime -= 0.1f;
				}
			}
		}
	}
}
