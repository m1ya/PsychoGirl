using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Psychokinesis : MonoBehaviour {

	public Camera raycamera;
	float timer;
	bool isLook;
	public Text text;
	GameObject Looked;
	GameObject beforeLooked;
	public GameObject titleCanvas;
	public GameObject gameoverCanvas;
	public AudioSource walk;
	public AudioSource psy;
	public AudioSource click;

	// Use this for initialization 
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

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
						if (Looked.name == "StartBlock") {
							titleCanvas.transform.FindChild ("Start").gameObject.GetComponent<Text> ().color = Color.white;
						}
						if (Looked.name == "RetryBlock") {
							gameoverCanvas.transform.FindChild("Retry").gameObject.GetComponent<Text> ().color = new Color (0.588f, 0, 0);
						}
						Looked.GetComponent<Renderer> ().material.color = new Color (Looked.GetComponent<Renderer> ().material.color.r, Looked.GetComponent<Renderer> ().material.color.g, Looked.GetComponent<Renderer> ().material.color.b, 1);
					}
					timer = 0;
					Looked = hitInfo.collider.gameObject;
					if (Looked.name != "StartBlock" && Looked.name != "RetryBlock") {
						if (!PlayerLevel.isStart || PlayerLevel.isGameOver) return;
						psy.Play ();
					}
				}
			} else {
				//もしEnemy以外を見たら
				if (Looked) {
					if (Looked.name == "StartBlock") {
						titleCanvas.transform.FindChild ("Start").gameObject.GetComponent<Text> ().color = Color.white;
					}
					if (Looked.name == "RetryBlock") {
						gameoverCanvas.transform.FindChild("Retry").gameObject.GetComponent<Text> ().color = new Color (0.588f, 0, 0);
					}
					Looked.GetComponent<Renderer> ().material.color = new Color (Looked.GetComponent<Renderer> ().material.color.r, Looked.GetComponent<Renderer> ().material.color.g, Looked.GetComponent<Renderer> ().material.color.b, 1);
				}
				psy.Stop ();
				Looked = null;
			}
			Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
		} else {
			//目を離したら透明度を戻してLookedをnullに
			if (Looked) {
				if (Looked.name == "StartBlock") {
					titleCanvas.transform.FindChild("Start").gameObject.GetComponent<Text> ().color = Color.white;
				}
				if (Looked.name == "RetryBlock") {
					gameoverCanvas.transform.FindChild("Retry").gameObject.GetComponent<Text> ().color = new Color (0.588f, 0, 0);
				}
				Looked.GetComponent<Renderer> ().material.color = new Color (Looked.GetComponent<Renderer> ().material.color.r, Looked.GetComponent<Renderer> ().material.color.g, Looked.GetComponent<Renderer> ().material.color.b, 1);
			}
			psy.Stop ();
			Looked = null;
		}

		//ものを見ていたら時間をカウント
		if (Looked) {
			timer += Time.deltaTime;
			//Start,Retryだった場合は相手の色を変える
			if (Looked.name == "StartBlock") {
				titleCanvas.transform.FindChild ("Start").gameObject.GetComponent<Text> ().color = new Color (0.588f, 0, 0);
			}
			if (Looked.name == "RetryBlock") {
				gameoverCanvas.transform.FindChild("Retry").gameObject.GetComponent<Text> ().color = Color.white;
			}
			Looked.GetComponent<Renderer> ().material.color -= new Color (0, 0, 0, 1) * Time.deltaTime / PlayerLevel.NeedTime;
			//NeedTime以上見たときの処理
			if (timer > PlayerLevel.NeedTime) {
				if (Looked.name == "chair7") {
					if (!PlayerLevel.isStart || PlayerLevel.isGameOver) return;
					//psy.Play ();
					Looked.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
					Looked.transform.position = new Vector3 (Random.Range (-1, 1), 6, transform.position.z + 4 * 10 / 4 * PlayerLevel.speed * PlayerLevel.NeedTime);
					Looked.transform.localEulerAngles = new Vector3 (-45, 45, 90);
					Looked.GetComponent<MeshRenderer> ().enabled = false;
				} else if (Looked.name == "Barrel") {
					//psy.Play ();
					if (!PlayerLevel.isStart || PlayerLevel.isGameOver) return;
					Looked.transform.position = new Vector3 (Random.Range (-1, 1), 1, transform.position.z + 4 * 10 / 3 * PlayerLevel.speed * PlayerLevel.NeedTime);
				} else if (Looked.name == "StartBlock") {
					StartCoroutine (start ());
				} else if (Looked.name == "RetryBlock") {
					StartCoroutine (retry ());
				} else {
					if (!PlayerLevel.isStart || PlayerLevel.isGameOver) return;
					//psy.Play ();
					Looked.transform.position = new Vector3 (Random.Range (-1, 1), 0, transform.position.z + 4 * 10 / 3 * PlayerLevel.speed * PlayerLevel.NeedTime);
				}
				//最後に空にする
				psy.Stop ();
				Looked = null;
			}
		//何も見ていなければtimerを初期化
		} else {
			psy.Stop ();
			timer = 0.0f;
		}
	
	}

	IEnumerator start(){
		click.Play ();
		titleCanvas.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		gameObject.transform.FindChild("unitychan").gameObject.GetComponent<Animator> ().SetBool ("isRunning", true);
		PlayerLevel.isStart = true;
		walk.Play ();
	}

	IEnumerator retry(){
		click.Play ();
		initnum ();
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Main");
	}

	void initnum(){
		//必要な見てる時間
		PlayerLevel.NeedTime = 3.0f;
		PlayerLevel.speed = 1f;
		//サイコキネシスの有効距離
		PlayerLevel.distance = 9 + PlayerLevel.speed;
		//ゲームオーバーしてるかどうかのフラグ
		PlayerLevel.isGameOver = false;
		//ゲームが始まっているかどうかのフラグ
		PlayerLevel.isStart = false;
		ScoreManager.score = 0;
	}
}
