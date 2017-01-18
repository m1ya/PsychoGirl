using UnityEngine;
using System.Collections;

public class PlayerRun : MonoBehaviour {

	float scoreWall = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerLevel.isGameOver)	return;

		transform.position += Vector3.forward * Time.deltaTime * PlayerLevel.speed;
		ScoreManager.score += PlayerLevel.speed;
		if(PlayerLevel.speed < 9.9){
			if (ScoreManager.score - scoreWall >= 1000) {
				PlayerLevel.speed += 0.2f;
				Debug.Log (PlayerLevel.speed);
				scoreWall = ScoreManager.score;
				if (PlayerLevel.NeedTime > 1) {
					PlayerLevel.NeedTime -= 0.1f;
				}
			}
		}
	}
}
