using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour {

	public Rigidbody _rigidbody;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerLevel.isGameOver || !PlayerLevel.isStart)	return;

		if (player.transform.position.z - this.transform.position.z > 10) {
			this.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
			this.transform.position = new Vector3 (Random.Range (-1, 1), 6, transform.position.z + 5 * 10 / 4 * PlayerLevel.speed * PlayerLevel.NeedTime);
			this.transform.localEulerAngles = new Vector3 (-45, 45, 90);
			this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		}
		if(this.transform.position.y < -0.5f)
			this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);

		if (this.transform.position.z - player.transform.position.z < PlayerLevel.NeedTime * PlayerLevel.speed * 3) {
			if (this.transform.position.y > 5.5f) {
				_rigidbody.constraints = RigidbodyConstraints.None;
				this.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			}
		}
	}
}
