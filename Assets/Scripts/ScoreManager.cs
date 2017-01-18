using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	//進んだ距離
	static public float score = 0;



	public Text text;

	void Update(){
		text.text = score.ToString("#");
	}
}
