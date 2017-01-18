using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Psychokinesis : MonoBehaviour {

	public Camera raycamera;
	float timer;
	bool isLook;
	public Text text;
	GameObject Looked;
	GameObject beforeLooked;

	// Use this for initialization 
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerLevel.isGameOver)	return;

		//Debug用（見ている秒数を表示）
		text.text = timer.ToString();

		//見ているかどうかの判定
		Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
		Ray ray = raycamera.ScreenPointToRay (center);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo, PlayerLevel.distance)) {
			//もしEnemyタグが付いていたらLookedにオブジェクトを代入
			if (hitInfo.collider.gameObject.tag == "Enemy") {
				//もし前に見ていたものと違うものを見ていたら透明度を戻して一旦timer=0にする
				if (Looked != hitInfo.collider.gameObject) {
					if (Looked) {
						Looked.GetComponent<Renderer> ().material.color = new Color (Looked.GetComponent<Renderer> ().material.color.r, Looked.GetComponent<Renderer> ().material.color.g, Looked.GetComponent<Renderer> ().material.color.b, 1);
					}
					timer = 0;
				}
				Looked = hitInfo.collider.gameObject;
			} else {
				//もしEnemy以外を見たら
				if (Looked) {
					Looked.GetComponent<Renderer> ().material.color = new Color (Looked.GetComponent<Renderer> ().material.color.r, Looked.GetComponent<Renderer> ().material.color.g, Looked.GetComponent<Renderer> ().material.color.b, 1);
				}
				Looked = null;
			}
			Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
		} else {
			//目を離したら透明度を戻してLookedをnullに
			if (Looked) {
				Looked.GetComponent<Renderer> ().material.color = new Color (Looked.GetComponent<Renderer> ().material.color.r, Looked.GetComponent<Renderer> ().material.color.g, Looked.GetComponent<Renderer> ().material.color.b, 1);
			}
			Looked = null;
		}

		//3秒以上見ていたらアクション
		if (Looked) {
			timer += Time.deltaTime;
			Looked.GetComponent<Renderer> ().material.color -= new Color (0, 0, 0, 1) * Time.deltaTime / PlayerLevel.NeedTime;
			if (timer > PlayerLevel.NeedTime) {
				if (Looked.name == "chair7") {
					Looked.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
					Looked.transform.position = new Vector3 (Random.Range (-1, 1), 6, transform.position.z + 4 * 10 / 4 * PlayerLevel.speed * PlayerLevel.NeedTime);
					Looked.transform.localEulerAngles = new Vector3 (-45, 45, 90);
				} else if (Looked.name == "Barrel") {
					Looked.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
					Looked.transform.position = new Vector3 (Random.Range (-1, 1), 1, transform.position.z + 4 * 10 / 3 * PlayerLevel.speed * PlayerLevel.NeedTime);
				} else {
					Looked.transform.position = new Vector3 (Random.Range (-1, 1), 0, transform.position.z + 4 * 10 / 3 * PlayerLevel.speed * PlayerLevel.NeedTime);
				}
				Looked = null;
			}
		} else {
			timer = 0.0f;
		}
	
	}
}
