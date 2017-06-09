using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

	//必要な見てる時間
	static public float NeedTime = 3.0f;
	//プレイヤーの歩行速度
	static public float speed = 1f;
	//サイコキネシスの有効距離
	static public float distance = 9 + speed;
	//ゲームオーバーしてるかどうかのフラグ
	static public bool isGameOver = false;
	//ゲームが始まっているかどうかのフラグ
	static public bool isStart = false;

}
